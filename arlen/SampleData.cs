using System.Linq;
using arlen.Models;
using System.Collections.Generic;

namespace arlen
{
    public static class SampleData
    {
        public static void Initialize(ArlenContext context)
        {
            if (!context.Users.Any())
            {
                User admin = new Models.User
                {
                    Login = "Admin",
                    Password = "Admin",
                    AccountEmail = "your.email@gmail.com",

                    Address = "Днепр, улица Глинки, 2",
                    Map = "<iframe src='https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d76411.84138045265!2d35.04288998962739!3d48.44466002067314!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x40dbfd39b627fa61%3A0x8691d382afae533b!2z0L4uINCo0LXQstGB0LrQuNC5!5e0!3m2!1sru!2sua!4v1536610671539' frameborder='0' style='border:0' allowfullscreen></iframe>",
                    Emails = "your.email@gmail.com",
                    Phones = "380953697277",
                    Files = "^|^|^",
                    Logo = "/images/logo.png",

                    Facebook = "https://facebook.com/",
                    Twitter = "https://twitter.com/",
                    Youtube = "https://youtube.com/",
                    Linkedin = "https://linkedin.com/",
                    Viber = "380953697277",
                    Whatsapp = "380953697277",

                    AboutImage = "/images/about-us.png",
                    AboutVideoMode = false,
                    AboutText = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    AboutVideo = ""
                };
                context.Users.Add(admin);
            }

            if (!context.Services.Any())
            {
                string file_stub = "|";
                Models.Service[] services = {
                new Models.Service{ Name = "Менеджмент",
                                    Text = "Текст для менеджмента",
                                    File =  file_stub},
                new Models.Service{ Name = "Зеленые услуги",
                                    Text = "Текст для зеленых",
                                    File =  file_stub},
                new Models.Service{ Name = "Маркетинг",
                                    Text = "Текст для маркетинга",
                                    File =  file_stub},
                new Models.Service{ Name = "Финансы",
                                    Text = "Текст для финансов",
                                    File =  file_stub},
                new Models.Service{ Name = "Инновации",
                                    Text = "Текст для инноваций",
                                    File =  file_stub},
                new Models.Service{ Name = "Инвестиции",
                                    Text = "Текст для инвестиций",
                                    File =  file_stub},
                new Models.Service{ Name = "Производство",
                                    Text = "Текст для производства",
                                    File =  file_stub},
                new Models.Service{ Name = "Персонал",
                                    Text = "Текст для персонала",
                                    File =  file_stub}
                };
                foreach (Models.Service svc in services)
                {
                    context.Services.Add(svc);
                }
            }

            if (!context.Promos.Any())
            {
                string promoImage = "/images/default-slider.jpg";
                Promo[] promos =
                {
                new Promo{ Text = "Уверенность в завтрашнем дне",
                           Image = promoImage},
                new Promo{ Text = "Уверенность в завтрашнем дне",
                           Image = promoImage},
                new Promo{ Text = "Уверенность в завтрашнем дне",
                           Image = promoImage},
                new Promo{ Text = "Уверенность в завтрашнем дне",
                           Image = promoImage},
                new Promo{ Text = "Уверенность в завтрашнем дне",
                           Image = promoImage},
                };
                foreach (Promo pm in promos)
                {
                    context.Promos.Add(pm);
                }
            }

            if (!context.Partners.Any())
            {
                Partner[] partners =
            {
                new Partner{ Image = "http://i0.wp.com/credit.kherson.ua/wp-content/uploads/2014/03/privatbank.png",
                              Link = "https://google.com/search?q=приватбанк" },
                new Partner{ Image = "http://www.interlink.co.th/libs/image/logo.png",
                              Link = "https://google.com/search?q=интерлинк" },
                new Partner{ Image = "http://i.biz-gid.com/img/logo/5682.jpeg",
                              Link = "https://google.com/search?q=ассоциация+грибопроизводителей+украины" },
                new Partner{ Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/TrekStor.svg/2000px-TrekStor.svg.png",
                              Link = "https://google.com/search?q=трекстор" },
                };
                foreach (Partner p in partners)
                {
                    context.Partners.Add(p);
                }
            }

            if (!context.NewsList.Any())
            {
                int number = 10;

                List<Models.News> news = new List<News>();
                System.DateTime createTime = System.DateTime.Now;
                string defaultImage = "/images/default-news-img.png";

                for (int i = 0; i < number; i++)
                {
                    news.Add(new News {
                        Title = "Тестовая новость " + i,
                        Content = "Тестовое содержание новости " + i,
                        CreateTime = createTime,
                        Images = defaultImage
                    });
                }
                foreach (News n in news)
                {
                    context.NewsList.Add(n);
                }
            }

            if (!context.Events.Any())
            {
                int number = 10;

                List<Models.Event> events = new List<Event>();
                System.DateTime createTime = System.DateTime.Now;
                System.DateTime date = System.DateTime.UtcNow;
                string defaultImage = "/images/default-event-img.png";

                for (int i = 0; i < number; i++)
                {
                    events.Add(new Event {
                        Title = "Тестовый ивент " + i,
                        Content = "Тестовое содержание ивента " + i,
                        CreateTime = createTime,
                        Date = date,
                        Place = "Условное место",
                        Images = defaultImage
                    });
                }
                foreach (Event e in events)
                {
                    context.Events.Add(e);
                }
            }

            context.SaveChanges();
        }
    }
}
