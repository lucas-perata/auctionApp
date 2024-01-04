using System;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope(); 

            SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
        }

        private static void SeedData(AuctionDbContext context)
        {
            context.Database.Migrate(); 

            if (context.Auctions.Any())
            {
                Console.WriteLine("Already have data"); 
                return; 
            }

            var auctions = new List<Auction>(){
                	    
            new Auction
            {
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(10),
                Item = new Item
                {
                    Title = "Sparkle Clean",
                    Description = "Professional cleaning for homes and offices",
                    ImageUrl = "https://media.istockphoto.com/id/1446816903/es/foto/servicio-de-limpieza-profunda-limpiador-profesional-de-lavado-de-suelo-blanco-en-sal%C3%B3n-de.jpg?s=2048x2048&w=is&k=20&c=fd9o0WOr3VtsFgdcXRXr2I7SganHgfWe3pEzS56-8ZM="
                }
            },
            new Auction
            {
                Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                Status = Status.Live,
                ReservePrice = 90000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(60),
                 Item = new Item
                {
                    Title = "Paws in Motion",
                    Description = "Reliable dog walking and pet sitting services",
                    ImageUrl = "https://media.istockphoto.com/id/1470365375/es/foto/los-perros-pastores-de-varios-tipos-de-perros-son-muy-lindos-en-el-camino.jpg?s=2048x2048&w=is&k=20&c=idtPapCmEbPc4b3teL9m_tTIEYhcgC0xFDuale1YqQA="
                }
            },
            new Auction
            {
                Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                Status = Status.Live,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(4),
                Item = new Item
                {
                    Title = "GreenThumb Landscapes",
                    Description = "Lawn mowing, landscaping, and garden maintenance",
                    ImageUrl = "https://media.istockphoto.com/id/1445233447/es/foto/jardinero-paisajista-colocando-c%C3%A9sped-para-el-nuevo-c%C3%A9sped.jpg?s=2048x2048&w=is&k=20&c=ZOM9-8fiAs_pXUDltEphAFuAutT78zAwDa5F53PHE_w="
                }
            },
            new Auction
            {
                Id = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
                Status = Status.ReserveNotMet,
                ReservePrice = 50000,
                Seller = "tom",
                AuctionEnd = DateTime.UtcNow.AddDays(-10),
                Item = new Item
                {
                    Title = "Gourmet Delights",
                    Description = "Customized meal preparation and catering",
                    ImageUrl = "https://media.istockphoto.com/id/1434365849/es/foto/mujer-emocionada-cantando-y-bailando-en-la-cocina-moderna-de-casa-mujer-feliz-sosteniendo.jpg?s=2048x2048&w=is&k=20&c=yDJ1YhQprGfiHsm5E0N8pfZG-ARamp3tZLmnxSjYKtQ="
                }
            },
            new Auction
            {
                Id = Guid.Parse("466e4744-4dc5-4987-aae0-b621acfc5e39"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(30),
                Item = new Item
                {
                    Title = "TaskMaster Services",
                    Description = "Get your errands done efficiently and promptly",
                    ImageUrl = "https://media.istockphoto.com/id/1493705743/es/foto/joven-enfermera-abrazando-a-su-clienta-mayor.jpg?s=2048x2048&w=is&k=20&c=EnrX9HnQeus3mtxdVXR4r5jQ0OBX_DcLCL5wGTJpaUI="
                }
            },
            new Auction
            {
                Id = Guid.Parse("dc1e4071-d19d-459b-b848-b5c3cd3d151f"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(45),
                Item = new Item
                {
                    Title = "Academic Ace",
                    Description = "Expert tutoring for various subjects and levels",
                    ImageUrl = "https://cdn.pixabay.com/photo/2020/01/22/09/40/teacher-4784917_1280.jpg"
                }
            },
            new Auction
            {
                Id = Guid.Parse("47111973-d176-4feb-848d-0ea22641c31a"),
                Status = Status.Live,
                ReservePrice = 150000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(13),
                Item = new Item
                {
                    Title = "TaskSwift Virtual Assistants",
                    Description = "Outsource your administrative tasks to skilled professionals",
                    ImageUrl = "https://media.istockphoto.com/id/1434661763/es/foto/retrato-de-una-joven-empresaria-exitosa-una-mujer-hispana-que-trabaja-dentro-de-un-moderno.jpg?s=2048x2048&w=is&k=20&c=68Z90YmC6rk5eB2rEwFvg2wwmnoOFGvFHSVtvxR8hUM="
                }
            },
            new Auction
            {
                Id = Guid.Parse("6a5011a1-fe1f-47df-9a32-b5346b289391"),
                Status = Status.Live,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(19),
                Item = new Item
                {
                    Title = "ShineWheels Auto Spa",
                    Description = "Comprehensive car detailing services for a showroom finish",
                    ImageUrl = "https://media.istockphoto.com/id/149298481/es/foto/esponja-en-el-coche-para-lavar.jpg?s=2048x2048&w=is&k=20&c=qXZXKjJKcphD5YMl5H5viBjIyvZb580c0WmGSiu1GSI="
                }
            },
            new Auction
            {
                Id = Guid.Parse("40490065-dac7-46b6-acc4-df507e0d6570"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "tom",
                AuctionEnd = DateTime.UtcNow.AddDays(20),
                Item = new Item
                {
                    Title = "FitFocus Training",
                    Description = "Individualized fitness training and wellness coaching",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/08/06/00/27/yoga-2587066_1280.jpg"
                }
            },
            new Auction
            {
                Id = Guid.Parse("3659ac24-29dd-407a-81f5-ecfe6f924b9b"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(48),
                Item = new Item
                {
                    Title = "DreamEvents Co.",
                    Description = "Full-service event planning for special occasions",
                    ImageUrl = "https://media.istockphoto.com/id/1391430740/es/foto/primer-plano-de-la-mujer-de-negocios-escribiendo-notas-a-mano-verificando-el-horario-del.jpg?s=2048x2048&w=is&k=20&c=OHumoAKMOjmYcDSdznbekn46LfonlQxPSPHZLGx9ml8="
                }
            } 
            };

            context.AddRange(auctions); 

            context.SaveChanges(); 
        }

    }
}
