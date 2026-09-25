using Application.Interface;
using CEIS.Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.CreateContent
{
    public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
    {
        public CreateContentCommandValidator()
        {
            RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title cannot exceed 200 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(10000)
                .WithMessage("Description cannot exceed 10000 characters.");

            RuleFor(c => c.Type)
                .IsInEnum()
                .WithMessage("Invalid content type.");

            RuleFor(c => c.Genre)
                .NotEmpty()
                .WithMessage("Genre is required.")
                .MaximumLength(100)
                .WithMessage("Genre cannot exceed 100 characters.");

            RuleFor(c => c.ThumbnailUrl)
                .NotEmpty()
                .WithMessage("Thumbnail URL is required.")
                .MaximumLength(500)
                .WithMessage("Thumbnail URL cannot exceed 500 characters.");

            RuleFor(c => c.MediaUrl)
                .NotEmpty()
                .WithMessage("Media URL is required.")
                .MaximumLength(500)
                .WithMessage("Media URL cannot exceed 500 characters.");

            RuleFor(c => c.ReleaseDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(c => c.ReleaseDate.HasValue)
                .WithMessage("Release date cannot be in the future.");

            RuleFor(c => c.CategoryId)
                .GreaterThan(0)
                .WithMessage("A valid category is required.");

        }
    }
}
