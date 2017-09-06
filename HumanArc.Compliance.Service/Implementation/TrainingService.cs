using System;
using System.Collections.Generic;
using System.Linq;
using HumanArc.Compliance.Data.Repositories;
using HumanArc.Compliance.Shared.Entities;
using HumanArc.Compliance.Shared.Interfaces;
using AutoMapper;
using HumanArc.Compliance.Shared.ViewModels;

namespace HumanArc.Compliance.Service.Implementation
{
    public class TrainingService : ITrainingService
    {
        private readonly TrainingRepository _repository;
        //private readonly UserRepository _userRepo;
        private readonly IMapper _mapper;
        public TrainingService(TrainingRepository repository, IMapper mapper)
        {
            _repository = repository;
            //_questionRepository = questionRepository;
            //_groupRepository = groupRepository;
            //_userRepo = userRepo;
            _mapper = mapper;
        }

        public TrainingViewModel GetById(int id)
        {
            var training = _repository.GetById(id);
            var trainingDc = _mapper.Map<Training, TrainingViewModel>(training);
            trainingDc.Questions = new List<QuestionViewModel>();
            if (training.Questions != null)
            {
                foreach (var question in training.Questions.Where(x => !x.Deleted))
                {
                    trainingDc.Questions.Add(_mapper.Map<Question, QuestionViewModel>(question));
                }
            }
            return trainingDc;
        }

        public List<TrainingSummaryViewModel> GetAll()
        {
            List<TrainingSummaryViewModel> list = new List<TrainingSummaryViewModel>();
            var all = _repository.GetAll();
            foreach (var training in all)
            {
                list.Add(new TrainingSummaryViewModel()
                {
                    ID = training.ID,
                    Name = training.Name,
                    Description = training.Description
                });
            }
            return list;
        } 

        public TrainingViewModel Save(TrainingViewModel model)
        {
            Training training = model.ID.HasValue ? _repository.GetById(model.ID.Value) : new Training();
            _mapper.Map<TrainingViewModel, Training>(model, training);

            /* TODO: Read in current user on Created to setup relationship
            var user = ActiveDirectoryHelper.GetCurrentUsersName();
            var dbUser = _userService.GetUserByUserName(user);

            if (dbUser == null)
            {
                UserViewModel newUser = new UserViewModel();
                newUser.DisplayName = user;
                newUser.UserName = user;
                vm.User = newUser;
            }
            else
            {
                vm.UserID = _mapper.Map<User, UserViewModel>(dbUser).ID;
            }*/

            if (model.ID.HasValue) _repository.Update(training);
            else _repository.Create(training);

            return _mapper.Map<Training, TrainingViewModel>(training);
        }

        public bool Delete(int id)
        {
            var training = _repository.GetById(id);
            if (training != null)
            {
                _repository.Delete(training);
                return true;
            }
            return false;
        }

        public TrainingViewModel SetMediaFileName(int id, string filename)
        {
            var training = _repository.GetById(id);
            if (training != null)
            {
                training.FilePathToMedia = filename;
                _repository.Update(training);
            }

            return _mapper.Map<Training, TrainingViewModel>(training);
        }

        //public TrainingViewModel SoftDeleteTraining(int ID)
        //{
        //    var training = _repository.GetById(ID);
        //    training.IsDeleted = true;
        //    _repository.Update(training);
        //    return _mapper.Map<Training, TrainingViewModel>(training);
        //}
        //public ADGroupViewModel AddGroup(ADGroupViewModel model)
        //{
        //    var foundGroup = _groupRepository.GetGroupByGroupName(model.GroupName);
        //    var training = _repository.GetById(model.TrainingID);

        //    _repository.AssociateAndSave(training.ID, foundGroup);

        //    return model;
        //}

        //public void DeleteGroup(ADGroupViewModel model)
        //{
        //    var group = GetGroupByGroupName(model.GroupName);
        //    model.ID = group.ID;
        //    var training = _repository.GetById(model.TrainingID);
        //    training.ADGroup.Remove(_mapper.Map<ADGroupViewModel, ADGroup>(model));
        //    _repository.Update(training);

        //    // delete the group
        //    //_groupRepository.Delete(_mapper.Map<ADGroupViewModel, ADGroup>(model));
        //}

        //public ADGroup GetGroupByGroupName(string groupName)
        //{
        //    return _groupRepository.GetGroupByGroupName(groupName);
        //}
        //public List<TrainingSummaryViewModel> GetListOfTraingingByUser(string user)
        //{
        //    //var userAccount = _userRepo.GetUserByUserName(user);
        //    var viewModelList = new List<TrainingSummaryViewModel>();

        //    var listOfTrainings =  _repository.GetAll();//.ToList().Where(x =>x.UserID == userAccount.ID).ToList();

        //    foreach(var trainings in listOfTrainings)
        //    {
        //        viewModelList.Add(_mapper.Map<Training, TrainingSummaryViewModel>(trainings));
        //    }

        //    return viewModelList;
        //}
    }
}
