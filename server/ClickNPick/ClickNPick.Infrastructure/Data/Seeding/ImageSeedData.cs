using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Infrastructure.Data.Seeding;

public class ImageSeedData
{
    public static readonly string AdminImageId = Guid.NewGuid().ToString();
    public static readonly string UserImageId = Guid.NewGuid().ToString();



    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>().HasData(
        new Image { Id = UserImageId, Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716827378/cool-profile-picture-87h46gcobjl5e4xu_mt6mhi.jpg", PublicId = "get634", UserId = UserSeedData.UserId },
        new Image { Id = AdminImageId, Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716829465/5e32f2a324306a19834af322_uhj3uq.jpg", PublicId = "ge32_gre_4", UserId = UserSeedData.UserAdminId },
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620405/Electronic_sonoa1.jpg", PublicId = "ixr4e_abc123" },
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620567/Number-of-Books-Published-Per-Year_icpoaj.jpg", PublicId = "12e_x7tc123" },
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620636/San-Diego-Plus-Size-Clothing-Stores_adgwqo.jpg", PublicId = "pht6781_xyz456" },
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620686/istock-1196974664_cbcqpa.jpg", PublicId = "pic_789abc" },
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620767/images_ylu4di.jpg", PublicId = "zy5h_x1t2n" },
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620828/alimentare-arredamento-2_rlgxvn.jpg", PublicId = "p3x4hge_456def"},
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620881/shutterstock_383521510-002-scaled_wqcefn.jpg", PublicId = "7845e_dbc123" },
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716620928/Heinens-Health-And-Beauty-products-800x550-1_lmvte5.jpg", PublicId = "p23c_123jkl" },
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716621012/Food_sqrw11.webp", PublicId = "53gfa_abc123"},
        new Image { Id = Guid.NewGuid().ToString(), Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1716621051/SupremeHomepageImageRight_ihrnvm.jpg", PublicId = "cht3_123jkl"}
        ); ;
    }
}
