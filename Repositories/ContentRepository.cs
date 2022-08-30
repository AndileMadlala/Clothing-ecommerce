using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using Ecommerce.Models;
using Ecommerce.ViewModel;

namespace Ecommerce.Repositories
{
    public class ContentRepository
    {
            private readonly ApplicationDbContext db = new ApplicationDbContext();
            public int UploadImageInDataBase(HttpPostedFileBase file, ContentViewModel contentViewModel)
            {
                contentViewModel.Image = ConvertToBytes(file);
                var Content = new Content
                {
                    Title = contentViewModel.Title,
                    Description = contentViewModel.Description,
                    Contents = contentViewModel.Contents,
                    Quantity = contentViewModel.Quantity,
                    Price = contentViewModel.Price,
                    Colour = contentViewModel.Colour,
                    size = contentViewModel.size,
                    total = contentViewModel.total,
                    bill = contentViewModel.bill,
                    Image = contentViewModel.Image
                    
                };
                db.Contents.Add(Content);
                int i = db.SaveChanges();
                if (i == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }

            public byte[] ConvertToBytes(HttpPostedFileBase image)
            {
                byte[] imageBytes = null;
                BinaryReader reader = new BinaryReader(image.InputStream);
                imageBytes = reader.ReadBytes((int)image.ContentLength);
                return imageBytes;
            }
        }
    }