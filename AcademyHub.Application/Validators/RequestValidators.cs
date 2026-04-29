using AcademyHub.Application.DTOs.Requests;
using FluentValidation;

namespace AcademyHub.Application.Validators
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Age).GreaterThan(0).LessThanOrEqualTo(150);
        }
    }

    public class UpdateStudentValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Age).GreaterThan(0).LessThanOrEqualTo(150);
        }
    }

    public class CreateClassValidator : AbstractValidator<CreateClassRequest>
    {
        public CreateClassValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Teacher).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }

    public class CreateEnrollmentValidator : AbstractValidator<CreateEnrollmentRequest>
    {
        public CreateEnrollmentValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty();
            RuleFor(x => x.ClassId).NotEmpty();
        }
    }

    public class RecordMarkValidator : AbstractValidator<RecordMarkRequest>
    {
        public RecordMarkValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty();
            RuleFor(x => x.ClassId).NotEmpty();
            RuleFor(x => x.ExamMark).InclusiveBetween(0, 100);
            RuleFor(x => x.AssignmentMark).InclusiveBetween(0, 100);
        }
    }
}
