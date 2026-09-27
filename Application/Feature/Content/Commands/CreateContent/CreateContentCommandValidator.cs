using Application.Common.Models;
using Application.Interface;
using Domain.Enum;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Content.Commands.CreateContent
{
    public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
    {
        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private static readonly string[] VideoExtensions = { ".mp4", ".mov", ".webm" };
        private static readonly string[] AudioExtensions = { ".mp3", ".wav" };
        private const long MaxThumbnailBytes = 30 * 1024 * 1024;    // 30MB
        private const long MaxMediaBytes = 500 * 1024 * 1024; // 500

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

            RuleFor(c => c.ReleaseDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .When(c => c.ReleaseDate.HasValue)
                .WithMessage("Release date cannot be in the future.");

            RuleFor(c => c.CategoryId)
                .GreaterThan(0)
                .WithMessage("A valid category is required.");

            When(x => x.ThumbnailFile is not null, () =>
            {
                RuleFor(x => x.ThumbnailFile)
                    .Must(HasExtension(ImageExtensions))
                    .WithMessage("Thumbnail must be in jpg, jpeg, png, or webp format.")
                    .Must(f => f!.Length <= MaxThumbnailBytes)
                    .WithMessage("Thumbnail size must not exceed 30MB.");
            });


            When(x => x.MediaFile is not null && x.Type == ContentType.Video, () =>
            {
                RuleFor(x => x.MediaFile)
                    .Must(HasExtension(VideoExtensions))
                    .WithMessage("Video must be in mp4, mov, or webm format.")
                    .Must(f => f!.Length <= MaxMediaBytes)
                    .WithMessage("Video size must not exceed 500MB.");
            });

            When(x => x.MediaFile is not null && x.Type == ContentType.Audio, () =>
            {
                RuleFor(x => x.MediaFile)
                    .Must(HasExtension(AudioExtensions))
                    .WithMessage("Audio must be in mp3 or wav format.")
                    .Must(f => f!.Length <= MaxMediaBytes)
                    .WithMessage("Audio size must not exceed 100MB.");
            });

            When(x => x.MediaFile is not null && x.Type == ContentType.Image, () =>
            {
                RuleFor(x => x.MediaFile)
                    .Must(HasExtension(ImageExtensions))
                    .WithMessage("Media image must be in jpg, jpeg, png, or webp format.")
                    .Must(f => f!.Length <= MaxThumbnailBytes)
                    .WithMessage("Image size must not exceed 30MB.");
            });
        }

        private static Func<IFormFile?, bool> HasExtension(string[] allowed) => file =>
        file != null && allowed.Contains(Path.GetExtension(file.FileName).ToLowerInvariant());
    }
}
