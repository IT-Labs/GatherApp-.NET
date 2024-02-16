using FluentValidation;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Requests;
using Microsoft.AspNetCore.Http;

namespace GatherApp.Contracts.Validators
{
    public class ImageValidator : AbstractValidator<UploadFileRequest>
    {
        public ImageValidator() 
        {
            RuleFor(x => x.ImageFile.Length).LessThanOrEqualTo(Values.FileSize)
                .WithMessage(Errors.FileMaxLength);

            RuleFor(x => x.ImageFile).Must(CheckFileType)
                .WithMessage(Errors.FileType);
        }

        private bool CheckFileType(IFormFile image)
        {
            return (image.ContentType.Equals(Values.JpegContentType) || image.ContentType.Equals(Values.JpgContentType) || image.ContentType.Equals(Values.PngContentType));
        }
    }
}
