using System;
using FluentValidation;
using Models.TaskDTOModels;

namespace Tasks.Validations
{
    public class TaskAddUpdateDTOModelValidator : AbstractValidator<TaskAddUpdateDTOModel>
    {
        public TaskAddUpdateDTOModelValidator()
        {
            // Name is required and must fit DB size
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

            // Id, when provided, must be a non-empty GUID
            RuleFor(x => x.Id)
                .Must(id => !id.HasValue || id.Value != Guid.Empty)
                .WithMessage("Id, when provided, must be a valid non-empty GUID.");

            // Description is optional but limit length to a reasonable max, in DB is MAX, but we can set a practical limit here
            RuleFor(x => x.Description)
                .MaximumLength(5000).WithMessage("Description must not exceed 5000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
