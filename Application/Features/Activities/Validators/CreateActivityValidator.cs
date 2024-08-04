using Application.Features.Activities.Commands;
using FluentValidation;

namespace Application.Features.Activities.Validators;

public class CreateActivityValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(300);

        RuleFor(x => x.Location)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(100);

        RuleFor(x => x.MainActivities)
            .NotNull()
            .NotEmpty()
            .ForEach(x => x.NotEmpty());


        RuleFor(x => x.Goals)
            .NotNull()
            .NotEmpty()
            .ForEach(x => x.NotEmpty());
        
        RuleFor(x => x.Scopes)
            .NotNull()
            .NotEmpty()
            .ForEach(x => x.SetValidator(new ActivityScopeValidator()));

        RuleFor(x => x.TotalSpots)
            .GreaterThan(1);

        RuleFor(x => x.StartDate)
            .GreaterThan(DateTime.UtcNow.AddHours(-6));

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate);
        
        RuleFor(x => x.Organizers)
            .NotNull()
            .NotEmpty()
            .ForEach(x => x.SetValidator(new ActivityOrganizersValidator()));

        RuleFor(x => x.ForeignCareersIds)
            .NotNull()
            .NotEmpty();
    }
}

public class ActivityScopeValidator : AbstractValidator<ActivityScopeRequest>
{
    public ActivityScopeValidator()
    {
        RuleFor(x => x.Scope)
            .IsInEnum();

        RuleFor(x => x.Hours)
            .GreaterThan(0);
    }
}

public class ActivityOrganizersValidator : AbstractValidator<ActivityOrganizerRequest>
{
    public ActivityOrganizersValidator()
    {
        When(x => x.Type is OrganizerType.Career, () =>
        {
            RuleFor(x => x.OrganizationId)
                .Null();

            RuleFor(x => x.CareerId)
                .NotNull();
        });

        When(x => x.Type is OrganizerType.Organization, () =>
        {
            RuleFor(x => x.CareerId)
                .Null();

            RuleFor(x => x.OrganizationId)
                .NotNull();
        });
    }
}