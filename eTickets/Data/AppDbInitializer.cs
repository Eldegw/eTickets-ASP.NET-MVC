using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using eTickets.Models;
using eTickets.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                // Cinemas
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema 1",
                            Logo = "/Images/Cinemas/cinema-1.jpg",
                            Description = "Grand Cinema City"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 2",
                            Logo = "/Images/Cinemas/cinema-2.jpg",
                            Description = "Skyline Movie Theater"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 3",
                            Logo = "/Images/Cinemas/cinema-3.jpg",
                            Description = "Royal IMAX Cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 4",
                            Logo = "/Images/Cinemas/cinema-4.jpg",
                            Description = "Star Plaza Cinemas"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 5",
                            Logo = "/Images/Cinemas/cinema-5.jpg",
                            Description = "Downtown Movie Hub"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 6",
                            Logo = "/Images/Cinemas/cinema-6.jpg",
                            Description = "Metro Cinema World"
                        }
                    });
                    context.SaveChanges();
                }

                // Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Christian Bale",
                            Bio = "Famous for his role in Batman.",
                            ProfilePictureURL = "/Images/Actors/actor-1.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Leonardo DiCaprio",
                            Bio = "Award-winning actor known for Inception.",
                            ProfilePictureURL = "/Images/Actors/actor-2.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Al Pacino",
                            Bio = "Legendary actor from The Godfather.",
                            ProfilePictureURL = "/Images/Actors/actor-3.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Vera Farmiga",
                            Bio = "Famous for horror movies like The Conjuring.",
                            ProfilePictureURL = "/Images/Actors/actor-4.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Tom Hanks",
                            Bio = "Beloved actor, voice of Woody in Toy Story.",
                            ProfilePictureURL = "/Images/Actors/actor-5.jpg"
                        }
                      
                    });
                    context.SaveChanges();
                }

                // Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Christopher Nolan",
                            Bio = "Visionary director and producer.",
                            ProfilePictureURL = "/Images/Producers/producer-1.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Emma Thomas",
                            Bio = "Prolific producer of blockbuster films.",
                            ProfilePictureURL = "/Images/Producers/producer-2.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Albert S. Ruddy",
                            Bio = "Producer of classic cinema masterpieces.",
                            ProfilePictureURL = "/Images/Producers/producer-3.jpg"
                        },
                        new Producer()
                        {
                            FullName = "James Wan",
                            Bio = "The mastermind behind modern horror.",
                            ProfilePictureURL = "/Images/Producers/producer-4.jpg"
                        },
                        new Producer()
                        {
                            FullName = "John Lasseter",
                            Bio = "Pioneer in animation and Pixar films.",
                            ProfilePictureURL = "/Images/Producers/producer-5.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Lynda Obst",
                            Bio = "Experienced producer in sci-fi and drama.",
                            ProfilePictureURL = "/Images/Producers/producer-6.jpg"
                        }
                    });
                    context.SaveChanges();
                }

                // Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "The Dark Knight",
                            Description = "Action masterpiece.",
                            Price = 39.5,
                            ImageURL = "/Images/Movies/movie-1.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "Inception",
                            Description = "A dream within a dream.",
                            Price = 29.5,
                            ImageURL = "/Images/Movies/movie-2.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            CinemaId = 2,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Drama
                        },
                       
                        new Movie()
                        {
                            Name = "The Conjuring",
                            Description = "Terrifying horror story.",
                            Price = 39.5,
                            ImageURL = "/Images/Movies/movie-3.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Toy Story",
                            Description = "World of talking toys.",
                            Price = 39.5,
                            ImageURL = "/Images/Movies/movie-4.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            CinemaId = 5,
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "Interstellar",
                            Description = "Epic space exploration.",
                            Price = 39.5,
                            ImageURL = "/Images/Movies/movie-5.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(15),
                            CinemaId = 6,
                            ProducerId = 6,
                            MovieCategory = MovieCategory.Documentary
                        }
                    });
                    context.SaveChanges();
                }

                // Actors_Movies
                if (!context.Actor_Movies.Any())
                {
                    context.Actor_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie() { ActorId = 1, MovieId = 1 },
                        new Actor_Movie() { ActorId = 2, MovieId = 1 },
                        new Actor_Movie() { ActorId = 2, MovieId = 2 },
                        new Actor_Movie() { ActorId = 3, MovieId = 2 },
                        new Actor_Movie() { ActorId = 3, MovieId = 3 },
                        new Actor_Movie() { ActorId = 2, MovieId = 4 },
                        new Actor_Movie() { ActorId = 3, MovieId = 4 },
                        new Actor_Movie() { ActorId = 4, MovieId = 4 },
                        new Actor_Movie() { ActorId = 2, MovieId = 5 },
                        new Actor_Movie() { ActorId = 5, MovieId = 5 },
                       
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}