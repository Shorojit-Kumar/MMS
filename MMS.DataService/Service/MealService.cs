﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MMS.DataService.IRepository;
using MMS.DataService.IService;
using MMS.Entities.DbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.DataService.Service
{
    public class MealService:IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MealService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
        }

        public async Task<Days> ChangeStatus(string dayId)
        {
            if (dayId == null)
            {
                throw new Exception("Invalid Route");
            }
           

            var day = await _unitOfWork.Days.GetById(Guid.Parse(dayId));
            if (day == null) throw new Exception("Invalid Route");

            if (day.IsEnd == false)
            {
                day.Breakfast = day.Breakfast > 0 ? 0 : 1;
                day.Lunch = day.Lunch > 0 ? 0 : 1;
                day.Dinner = day.Dinner > 0 ? 0 : 1;
                day.UpdatedAt = DateTime.Now;
                _unitOfWork.Days.Update(day);
                await _unitOfWork.CompleteAsync();
            }

            return day;

        }



        public async Task<bool> CloseTheDay(string MonthId, string MessId, string DayNo,string id)
        {
            if (MonthId == null || MessId == null || DayNo == null)
            {
                 throw new Exception("Invalid Route");
            }
            var manager = await _unitOfWork.MessHaveMembers.GetByMessIdAndPersonId(Guid.Parse(MessId), Guid.Parse(id));
            if (manager == null || manager.IsManager == false) throw new Exception("Access denied");

            await _unitOfWork.Days.ChangeDayStatusByMonthIdAndDayNo(Guid.Parse(MonthId), Convert.ToInt32(DayNo));
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Days> UpdateMealStatus(string DayId, int BreakFast=0, int Lunch = 0 , int Dinner= 0)
        {
            var day = await _unitOfWork.Days.GetById(Guid.Parse(DayId));
            if(day is null)
            {
                throw new Exception("Invalid Route");
            }

            day.Breakfast = BreakFast != 0 ? day.Breakfast + BreakFast : day.Breakfast;
            day.Lunch = Lunch != 0 ? day.Lunch + Lunch : day.Lunch;
            day.Dinner = Dinner != 0 ? day.Dinner + Dinner : day.Dinner;

            day.Breakfast= day.Breakfast<0? 0 : day.Breakfast;
            day.Lunch = day.Lunch < 0 ? 0 : day.Lunch;
            day.Dinner = day.Dinner < 0 ? 0 : day.Dinner;
            day.UpdatedAt = DateTime.Now;

            _unitOfWork.Days.Update(day);
            await _unitOfWork.CompleteAsync();

            return day;

        }
    }
}