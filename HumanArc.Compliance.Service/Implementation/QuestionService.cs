using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HumanArc.Compliance.Data.Repositories;
using HumanArc.Compliance.Shared.Entities;
using HumanArc.Compliance.Shared.Interfaces;
using HumanArc.Compliance.Shared.ViewModels;


namespace HumanArc.Compliance.Service.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly QuestionRepository _repository;
        private readonly IMapper _mapper;
        public QuestionService(QuestionRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public QuestionViewModel GetById(int id)
        {
            var question = _repository.GetById(id);
            return _mapper.Map<Question, QuestionViewModel>(question);
        }

        public List<QuestionViewModel> GetQuestionsByTrainingId(int id)
        {
            var questions = _repository.GetAll().Where(x => x.TrainingId == id && !x.Deleted);
            return questions.Select(question => _mapper.Map<Question, QuestionViewModel>(question)).ToList();
        }

        public QuestionViewModel Save(QuestionViewModel model)
        {
            Question question = model.ID.HasValue ? _repository.GetById(model.ID.Value) : new Question();
            _mapper.Map<QuestionViewModel, Question>(model, question);

            if (model.ID.HasValue) _repository.Update(question);
            else _repository.Create(question);

            return _mapper.Map<Question, QuestionViewModel>(question);
        }

        public bool Delete(int id)
        {
            var question = _repository.GetById(id);
            _repository.Delete(question);
            return true;
        }
    }
}
