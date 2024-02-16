﻿using GatherApp.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GatherApp.DataContext
{
    public static class SeedCountryData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = new GatherAppContext(serviceProvider.GetRequiredService<DbContextOptions<GatherAppContext>>());

            if (context.Countries.Any())
                return;

            IList<Country> countries = new List<Country>()
            {
                new Country() { Name = "United States" },
                new Country() { Name = "Afghanistan" },
                new Country() { Name = "Albania" },
                new Country() { Name = "Algeria" },
                new Country() { Name = "Andorra" },
                new Country() { Name = "Angola" },
                new Country() { Name = "Antigua & Deps" },
                new Country() { Name = "Argentina" },
                new Country() { Name = "Armenia" },
                new Country() { Name = "Australia" },
                new Country() { Name = "Austria" },
                new Country() { Name = "Azerbaijan" },
                new Country() { Name = "Bahamas" },
                new Country() { Name = "Bahrain" },
                new Country() { Name = "Bangladesh" },
                new Country() { Name = "Barbados" },
                new Country() { Name = "Belarus" },
                new Country() { Name = "Belgium" },
                new Country() { Name = "Belize" },
                new Country() { Name = "Benin" },
                new Country() { Name = "Bhutan" },
                new Country() { Name = "Bolivia" },
                new Country() { Name = "Bosnia & Herzegovina" },
                new Country() { Name = "Botswana" },
                new Country() { Name = "Brazil" },
                new Country() { Name = "Brunei" },
                new Country() { Name = "Bulgaria" },
                new Country() { Name = "Burkina" },
                new Country() { Name = "Burma" },
                new Country() { Name = "Burundi" },
                new Country() { Name = "Cambodia" },
                new Country() { Name = "Cameroon" },
                new Country() { Name = "Canada" },
                new Country() { Name = "Cape Verde" },
                new Country() { Name = "Central African Republic" },
                new Country() { Name = "Chad" },
                new Country() { Name = "Chile" },
                new Country() { Name = "China" }, // P.R. of China
                new Country() { Name = "Taiwan" }, // Taiwan
                new Country() { Name = "Colombia" },
                new Country() { Name = "Comoros" },
                new Country() { Name = "Democratic Republic of the Congo" },
                new Country() { Name = "Republic of the Congo" },
                new Country() { Name = "Costa Rica" },
                new Country() { Name = "Croatia" },
                new Country() { Name = "Cuba" },
                new Country() { Name = "Cyprus" },
                new Country() { Name = "Czech Republic" },
                new Country() { Name = "Denmark" },
                new Country() { Name = "Djibouti" },
                new Country() { Name = "Dominica" },
                new Country() { Name = "Dominican Republic" },
                new Country() { Name = "East Timor" },
                new Country() { Name = "Ecuador" },
                new Country() { Name = "Egypt" },
                new Country() { Name = "El Salvador" },
                new Country() { Name = "Equatorial Guinea" },
                new Country() { Name = "Eritrea" },
                new Country() { Name = "Estonia" },
                new Country() { Name = "Ethiopia" },
                new Country() { Name = "Fiji" },
                new Country() { Name = "Finland" },
                new Country() { Name = "France" },
                new Country() { Name = "Gabon" },
                new Country() { Name = "The Gambia" },
                new Country() { Name = "Georgia" },
                new Country() { Name = "Germany" },
                new Country() { Name = "Ghana" },
                new Country() { Name = "Greece" },
                new Country() { Name = "Grenada" },
                new Country() { Name = "Guatemala" },
                new Country() { Name = "Guinea" },
                new Country() { Name = "Guinea-Bissau" },
                new Country() { Name = "Guyana" },
                new Country() { Name = "Haiti" },
                new Country() { Name = "Honduras" },
                new Country() { Name = "Hungary" },
                new Country() { Name = "Iceland" },
                new Country() { Name = "India" },
                new Country() { Name = "Indonesia" },
                new Country() { Name = "Iran" },
                new Country() { Name = "Iraq" },
                new Country() { Name = "Republic of Ireland" },
                new Country() { Name = "Israel" },
                new Country() { Name = "Italy" },
                new Country() { Name = "Ivory Coast" },
                new Country() { Name = "Jamaica" },
                new Country() { Name = "Japan" },
                new Country() { Name = "Jordan" },
                new Country() { Name = "Kazakhstan" },
                new Country() { Name = "Kenya" },
                new Country() { Name = "Kiribati" },
                new Country() { Name = "North Korea" },
                new Country() { Name = "South Korea" },
                new Country() { Name = "Kosovo" },
                new Country() { Name = "Kuwait" },
                new Country() { Name = "Kyrgyzstan" },
                new Country() { Name = "Laos" },
                new Country() { Name = "Latvia" },
                new Country() { Name = "Lebanon" },
                new Country() { Name = "Lesotho" },
                new Country() { Name = "Liberia" },
                new Country() { Name = "Libya" },
                new Country() { Name = "Liechtenstein" },
                new Country() { Name = "Lithuania" },
                new Country() { Name = "Luxembourg" },
                new Country() { Name = "North Macedonia" },
                new Country() { Name = "Madagascar" },
                new Country() { Name = "Malawi" },
                new Country() { Name = "Malaysia" },
                new Country() { Name = "Maldives" },
                new Country() { Name = "Mali" },
                new Country() { Name = "Malta" },
                new Country() { Name = "Marshall Islands" },
                new Country() { Name = "Mauritania" },
                new Country() { Name = "Mauritius" },
                new Country() { Name = "Mexico" },
                new Country() { Name = "Micronesia" },
                new Country() { Name = "Moldova" },
                new Country() { Name = "Monaco" },
                new Country() { Name = "Mongolia" },
                new Country() { Name = "Montenegro" },
                new Country() { Name = "Morocco" },
                new Country() { Name = "Mozambique" },
                new Country() { Name = "Namibia" },
                new Country() { Name = "Nepal" },
                new Country() { Name = "Netherlands" },
                new Country() { Name = "New Zealand" },
                new Country() { Name = "Nicaragua" },
                new Country() { Name = "Niger" },
                new Country() { Name = "Nigeria" },
                new Country() { Name = "Norway" },
                new Country() { Name = "Oman" },
                new Country() { Name = "Pakistan" },
                new Country() { Name = "Panama" },
                new Country() { Name = "Papua New Guinea" },
                new Country() { Name = "Paraguay" },
                new Country() { Name = "Peru" },
                new Country() { Name = "Philippines" },
                new Country() { Name = "Poland" },
                new Country() { Name = "Portugal" },
                new Country() { Name = "Qatar" },
                new Country() { Name = "Romania" },
                new Country() { Name = "Russian Federation" },
                new Country() { Name = "Rwanda" },
                new Country() { Name = "Samoa" },
                new Country() { Name = "San Marino" },
                new Country() { Name = "Sao Tome & Principe" },
                new Country() { Name = "Saudi Arabia" },
                new Country() { Name = "Senegal" },
                new Country() { Name = "Serbia" },
                new Country() { Name = "Seychelles" },
                new Country() { Name = "Sierra Leone" },
                new Country() { Name = "Singapore" },
                new Country() { Name = "Slovakia" },
                new Country() { Name = "Slovenia" },
                new Country() { Name = "Solomon Islands" },
                new Country() { Name = "Somalia" },
                new Country() { Name = "South Africa" },
                new Country() { Name = "Spain" },
                new Country() { Name = "Sri Lanka" },
                new Country() { Name = "Sudan" },
                new Country() { Name = "Suriname" },
                new Country() { Name = "Swaziland (Eswatini)" },
                new Country() { Name = "Sweden" },
                new Country() { Name = "Switzerland" },
                new Country() { Name = "Syria" },
                new Country() { Name = "Tajikistan" },
                new Country() { Name = "Tanzania" },
                new Country() { Name = "Thailand" },
                new Country() { Name = "Togo" },
                new Country() { Name = "Tonga" },
                new Country() { Name = "Trinidad & Tobago" },
                new Country() { Name = "Tunisia" },
                new Country() { Name = "Turkey" },
                new Country() { Name = "Turkmenistan" },
                new Country() { Name = "Uganda" },
                new Country() { Name = "Ukraine" },
                new Country() { Name = "United Arab Emirates" },
                new Country() { Name = "United Kingdom" },
                new Country() { Name = "Uruguay" },
                new Country() { Name = "Uzbekistan" },
                new Country() { Name = "Vatican City" },
                new Country() { Name = "Venezuela" },
                new Country() { Name = "Vietnam" },
                new Country() { Name = "Yemen" },
                new Country() { Name = "Zambia" },
                new Country() { Name = "Zimbabwe" },
            };

            context.Countries.AddRange(countries);
            context.SaveChanges();
        }
    }
}
