using Moq;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostingManagement.Application.UnitTests.Mocks
{
    public class TriggerRepositoryMocks
    {
        public static Mock<ITriggerRepository> GetTriggerRepository()
        {
            var triggerList = new List<Trigger>
            {
                new Trigger()
                {
                   TriggerId=1,
                   ScaleId =2,
                   Tenure = 32,
                   Mandatory="Yes"
                },
                 new Trigger()
                {
                   TriggerId=2,
                   ScaleId =1,
                   Tenure = 30,
                   Mandatory="No"
                },
                  new Trigger()
                {
                   TriggerId=3,
                   ScaleId =2,
                   Tenure = 24,
                   Mandatory="No"
                }
            };

            var scaleList = new List<Scale>()
            {
                new Scale()
                {
                    ScaleId=1,
                    Name = "Scale1"
                },
                new Scale()
                {
                    ScaleId=2,
                    Name = "Scale2"
                }
            };

            var mockTriggerRepository = new Mock<ITriggerRepository>();

            mockTriggerRepository.Setup(repo => repo.GetAllTriggerDetails()).ReturnsAsync(
                () =>
                {
                    var trigger = (from trigg in triggerList
                                   join scale in scaleList on trigg.ScaleId equals scale.ScaleId
                                   select new TriggerVm()
                                   {
                                       TriggerId = trigg.TriggerId,
                                       ScaleName = scale.Name,
                                       Tenure = trigg.Tenure,
                                       Mandatory = trigg.Mandatory
                                   }).OrderByDescending(x => x.TriggerId).ToList();
                    return trigger;
                }
                );

            mockTriggerRepository.Setup(repo => repo.GetTriggerDetailsById(It.IsAny<int>())).ReturnsAsync(
            (int triggerId) =>
            {
                var trigger = triggerList.FirstOrDefault(x => x.TriggerId == triggerId);
                return trigger;
            });

            mockTriggerRepository.Setup(repo => repo.AddTrigger(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(
                (int scaleId, int Tenure, string mandatoryStatus) =>
                {
                    if (!scaleList.Any(x=>x.ScaleId== scaleId))
                    {
                        return false;
                    }
                    Trigger triggerModel = new Trigger();
                    triggerModel.TriggerId = triggerList.Last().TriggerId + 1;
                    triggerModel.ScaleId = scaleId;
                    triggerModel.Tenure = Tenure;
                    triggerModel.Mandatory = mandatoryStatus;
                    triggerList.Add(triggerModel);

                    return true;
                });

            mockTriggerRepository.Setup(repo => repo.DeleteTrigger(It.IsAny<int>())).ReturnsAsync(
                (int triggerId) =>
                {
                    var trigger = triggerList.FirstOrDefault(x => x.TriggerId == triggerId);
                    if (trigger != null)
                    {
                        triggerList.Remove(trigger);
                        return true;
                    }
                    return false;
                });

            mockTriggerRepository.Setup(repo => repo.UpdateTrigger(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(
                (int triggerId, int scaleId, int tenure, string mandatoryStatus) =>
                {
                    if (!scaleList.Any(x => x.ScaleId == scaleId))
                    {
                        return false;
                    }
                    var oldTrigger = triggerList.First(x => x.TriggerId == triggerId);
                    var index = triggerList.IndexOf(oldTrigger);
                    if (index != -1)
                    {
                        oldTrigger.Tenure = tenure;
                        oldTrigger.ScaleId = scaleId;
                        oldTrigger.Mandatory = mandatoryStatus;
                        triggerList[index] = oldTrigger;
                        return true;
                    }
                    return false;
                });

            return mockTriggerRepository;

        }
    }
}
