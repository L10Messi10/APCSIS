using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Maui.Storage;

namespace APCSIS.Includes
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService()
        {
            var account = new Account(
                "dxm5py1o9",
                "988947236754142",
                "DRnz-RR1rOgl12bdMhdTchT4Ybo"
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImage(FileResult file)
        {
            if (file == null)
                return null;

            await using var stream = await file.OpenReadAsync();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = Guid.NewGuid().ToString(),
                Overwrite = true,
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }

        public string GetImageUrl(string publicId)
        {
            return _cloudinary.Api.UrlImgUp.BuildUrl(publicId);
        }
    }
}
