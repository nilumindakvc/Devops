using System.Security.Cryptography;
using System.Text;
using agent;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(agentDbContextSqlServer context)
    {
        // Seed Regions
        if (!context.Regions.Any())
        {
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[Regions] ON;
                INSERT INTO [Regions] ([RegionId], [RegionName], [Description], [IsActive], [CreatedAt])
                VALUES
                    (1, 'Middle-East', 'desert-countries', 1, '2025-07-02T01:36:57.0015947'),
                    (2, 'Europe', 'western_part', 1, '2025-07-03T14:24:25.1282518'),
                    (3, 'East-Asia', 'Asian-Gients', 1, '2025-07-03T14:42:44.4284344'),
                    (4, 'South Koria', 'down side and world connected part of the koria', 1, '2025-08-06T08:35:42.7900507');
                SET IDENTITY_INSERT [dbo].[Regions] OFF;
            ");
        }

        // Seed Countries
        if (!context.Countries.Any())
        {
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[Countries] ON;
                INSERT INTO [Countries] ([CountryId], [CountryName], [CountryCode], [RegionId], [IsActive], [CreatedAt])
                VALUES
                    (1, 'Dubai', 'ME100', 1, 1, '2025-07-02T08:04:53.9762645'),
                    (2, 'Kuwait', 'ME102', 1, 1, '2025-07-03T09:09:48.4445395'),
                    (3, 'SaudiArbia', 'ME103', 1, 1, '2025-07-03T09:10:28.3295418'),
                    (4, 'Germany', 'EU100', 2, 1, '2025-07-03T14:28:44.4728530'),
                    (5, 'Netherlands', 'EU101', 2, 1, '2025-07-03T14:29:21.4206816'),
                    (6, 'Ireland', 'EU102', 2, 1, '2025-07-03T14:29:46.2624312'),
                    (7, 'Sweden', 'EU103', 2, 1, '2025-07-03T14:30:09.9486966'),
                    (8, 'Denmark', 'EU104', 2, 1, '2025-07-03T14:30:28.0476410'),
                    (9, 'Norway', 'EU105', 2, 1, '2025-07-03T14:31:00.0270820'),
                    (10, 'Finland', 'EU106', 2, 1, '2025-07-03T14:31:35.4261834'),
                    (11, 'Poland', 'EU107', 2, 1, '2025-07-03T14:31:51.1836453'),
                    (12, 'Qatar', 'ME105', 1, 1, '2025-07-03T14:35:12.0758093'),
                    (13, 'Bahrain', 'ME106', 1, 1, '2025-07-03T14:35:27.1020842'),
                    (14, 'Yemen', 'ME107', 1, 1, '2025-07-03T14:35:42.9991990'),
                    (15, 'Jordan', 'ME108', 1, 1, '2025-07-03T14:37:07.9192702'),
                    (16, 'Lebanon', 'ME109', 1, 1, '2025-07-03T14:37:21.5743829'),
                    (17, 'Syria', 'ME110', 1, 1, '2025-07-03T14:37:40.1507702'),
                    (18, 'Iraq', 'ME111', 1, 1, '2025-07-03T14:37:57.9771272'),
                    (20, 'Japan', 'EA100', 3, 1, '2025-07-03T14:43:46.2453392'),
                    (21, 'South Korea', 'EA101', 4, 1, '2025-07-03T14:44:26.0926044'),
                    (22, 'Taiwan', 'EA102', 3, 1, '2025-07-03T14:44:49.7894385'),
                    (23, 'China', 'EA103', 3, 1, '2025-07-03T14:45:04.9161534'),
                    (24, 'Macau', 'EA104', 3, 1, '2025-07-03T14:45:18.5626246'),
                    (25, 'Hong Kong', 'EA105', 3, 1, '2025-07-03T14:45:34.3018262');
                SET IDENTITY_INSERT [dbo].[Countries] OFF;
            ");
        }

        // Seed Job Categories
        if (!context.JobCategories.Any())
        {
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[JobCategories] ON;
                INSERT INTO [JobCategories] ([CategoryId], [CategoryName], [Description], [IsActive])
                VALUES
                    (2, 'Healthcare', 'Healthcare Services vacancies', 1),
                    (3, 'Construction', 'short time job oppertunities in construction areas', 1),
                    (4, 'Hospitality', 'vacancies in hotels and resturents', 1),
                    (5, 'Logistics', 'All kind of Logistic job oppertunities', 1),
                    (6, 'Clean and Domestic', 'serve as home servers and cleaning related jobs in entities', 1);
                SET IDENTITY_INSERT [dbo].[JobCategories] OFF;
            ");
        }

        // Seed Agencies
        if (!context.Agencies.Any())
        {
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[Agencies] ON;
                INSERT INTO [Agencies] ([AgencyId], [AgencyName], [LicenseNumber], [Email], [Phone], [Address], [City], [Country], [Website], [Description], [RegistrationStatus], [RegistrationDate], [ApprovedDate], [IsActive], [AverageRating], [TotalReviews], [CreatedAt], [UpdatedAt], [Doc1Path], [Doc2Path], [LogoPath], [Password])
                VALUES
                    (5, 'Global Manpower Solutions', 'SLBFE3456', 'info@globalmanpower.lk', '+94 112 345 678', 'No. 25, Galle Road', 'Colombo', 'Sri Lanka', 'https://www.globalmanpower.lk', 'Leading overseas recruitment agency specializing in Middle East and Europe placements. ', 0, '2025-07-04T09:54:53.2377464', NULL, 0, 3.00, 0, '2025-07-04T09:54:53.2377466', '2025-07-04T09:54:53.2377466', '/app/wwwroot/doc1/8fbdf61f-39a3-4b01-b630-427cb1006f9c.png', '/app/wwwroot/doc2/7644c6a7-8e91-43f9-adaf-1ae551a003fe.png', '/app/wwwroot/logo/d7172d80-36f1-4395-b4f6-3f23deac57c8.png', 'password'),
                    (6, 'Emerald International', 'SLBFE2978', 'contact@emeraldintl.lk', '+94 112 556 789', 'No. 80, Negombo Road', 'Wattala', 'Sri Lanka', 'https://www.emeraldintl.lk', 'Authorized recruiting agency offering skilled and semi-skilled opportunities abroad.', 0, '2025-07-04T09:55:28.9881700', NULL, 0, 2.00, 0, '2025-07-04T09:55:28.9881701', '2025-07-04T09:55:28.9881702', '/app/wwwroot/doc1/42dedb06-db6e-4414-b082-d865369c223a.png', '/app/wwwroot/doc2/cf770410-0f21-4849-a0b4-28bf124e7ab3.png', '/app/wwwroot/logo/8eec3c8b-4433-4121-ab3e-4c016e2c8ee0.png', 'password'),
                    (7, 'Ceylon Recruiters Pvt Ltd', 'SLBFE1593', 'recruit@ceylonrecruiters.lk', '+94 114 789 123', 'No. 12A, Kandy Road', 'Kiribathgoda', 'Sri Lanka', 'https://www.ceylonrecruiters.lk', 'Accredited foreign employment agency with a focus on Gulf and Asian markets.', 0, '2025-07-04T09:55:56.0523533', NULL, 0, 1.00, 0, '2025-07-04T09:55:56.0523533', '2025-07-04T09:55:56.0523533', '/app/wwwroot/doc1/554790ee-5168-4555-adca-45b1b407bfb5.pdf', '/app/wwwroot/doc2/38e3c55e-095e-4736-8fab-d931797f70e4.pdf', '/app/wwwroot/logo/9667a0b8-79bf-48da-8078-e0b54b1523f0.jpg', 'password'),
                    (8, 'Oceanic Job Links', 'SLBFE1087', 'jobs@oceanic.lk', '+94 112 678 901', 'No. 48, Marine Drive', 'Dehiwala', 'Sri Lanka', 'https://www.oceanic.lk', 'Trusted manpower agency connecting Sri Lankan workers to Europe and the Far East.', 0, '2025-07-04T09:56:17.2019606', NULL, 0, 5.00, 0, '2025-07-04T09:56:17.2019606', '2025-07-04T09:56:17.2019607', '/app/wwwroot/doc1/316bd870-3279-4abb-8e2a-c89cb8d6c88a.pdf', '/app/wwwroot/doc2/9c6c711f-9a76-4bfe-9e6d-8845f0b5c39c.pdf', '/app/wwwroot/logo/07e1449f-9467-41ba-b104-034a398dd6e9.jpg', 'password'),
                    (9, 'Skyline Overseas Employment', 'SLBFE2120', 'support@skyline.lk', '+94 115 345 210', 'No. 101, High Level Road', 'Nugegoda', 'Sri Lanka', 'https://www.skyline.lk', 'Licensed foreign employment agency with operations in multiple overseas labor markets.', 0, '2025-07-04T09:56:33.4648021', NULL, 0, 2.00, 0, '2025-07-04T09:56:33.4648021', '2025-07-04T09:56:33.4648021', '/app/wwwroot/doc1/799e2a05-e855-44aa-ab3f-4e8e2d924f9d.jpg', '/app/wwwroot/doc2/cc9b198f-a722-4d2a-af6e-a41e78babe79.jpg', '/app/wwwroot/logo/ff45e22e-9b6f-4ca8-b844-e6a80609ca1d.jpg', 'password'),
                    (10, 'Lanka Talent Force', 'SLBFE3764', 'info@lankatalent.lk', '+94 117 123 456', 'No. 43, Dharmapala Mawatha', 'Colombo 07', 'Sri Lanka', 'https://www.lankatalent.lk', 'Providing expert recruitment services for health, hospitality, and technical sectors overseas.', 0, '2025-07-04T09:59:45.6626535', NULL, 0, 2.00, 0, '2025-07-04T09:59:45.6626535', '2025-07-04T09:59:45.6626535', '/app/wwwroot/doc1/ef81d47c-cb1a-40ce-8bb9-17e20a934786.jpg', '/app/wwwroot/doc2/3b5751c1-02b4-4274-af09-ae774f031759.jpg', '/app/wwwroot/logo/b35d5ca4-305f-44cb-8805-727eb51ade00.jpg', 'password'),
                    (11, 'Star Link Manpower', 'SLBFE2215', 'apply@starlinkmanpower.lk', '+94 112 876 543', 'No. 58, Gampaha Road', 'Ja-Ela', 'Sri Lanka', 'https://www.starlinkmanpower.lk', 'Well-reputed manpower agency deploying skilled workers to Gulf and Southeast Asia.', 0, '2025-07-04T10:00:19.6785334', NULL, 0, 1.00, 0, '2025-07-04T10:00:19.6785335', '2025-07-04T10:00:19.6785335', '/app/wwwroot/doc1/cd2e317d-9953-43c4-8fd3-9026d7ff109b.png', '/app/wwwroot/doc2/499d7f5a-035c-454f-abb5-fc940c3b02a7.png', '/app/wwwroot/logo/70606313-4fbc-4748-82c3-81bc6560e290.png', 'password'),
                    (12, 'Elite Global Recruiters', 'SLBFE1908', 'recruitment@eliteglobal.lk', '+94 114 765 432', 'No. 15, Havelock Road', 'Colombo 05', 'Sri Lanka', 'https://www.eliteglobal.lk', 'Specialized in overseas placements for IT, engineering, and hospitality sectors.', 0, '2025-07-04T10:00:37.7466477', NULL, 0, 3.00, 0, '2025-07-04T10:00:37.7466477', '2025-07-04T10:00:37.7466478', '/app/wwwroot/doc1/2dbf724b-5ac8-4ec5-9668-85d4c935569f.png', '/app/wwwroot/doc2/d8ec0002-3898-423b-84a9-0a491929895e.png', '/app/wwwroot/logo/3eff1917-114b-4803-8205-fa869736122e.jpg', 'password'),
                    (13, 'Blue Ocean Employment Services', 'SLBFE2880', 'careers@blueocean.lk', '+94 113 222 888', 'No. 67, Old Kottawa Road', 'Nugegoda', 'Sri Lanka', 'https://www.blueocean.lk', 'Connecting Sri Lankan professionals with premium employers in Europe and the Middle East.', 0, '2025-07-04T10:00:56.1107403', NULL, 0, 5.00, 0, '2025-07-04T10:00:56.1107404', '2025-07-04T10:00:56.1107404', '/app/wwwroot/doc1/934282ab-2e8c-46e9-aeb5-89c98ab348da.pdf', '/app/wwwroot/doc2/b1aba71c-e034-4bcf-a1d4-b792317736d6.pdf', '/app/wwwroot/logo/77f4461d-d22e-4652-8860-ea4bcca9caab.jpg', 'password'),
                    (14, 'TransAsia Manpower', 'SLBFE1333', 'jobs@transasia.lk', '+94 112 334 556', 'No. 34, Hospital Road', 'Kalutara', 'Sri Lanka', 'https://www.transasia.lk', 'Government-approved agency providing end-to-end recruitment support for global opportunities.', 0, '2025-07-04T10:01:09.0258193', NULL, 0, 4.00, 0, '2025-07-04T10:01:09.0258193', '2025-07-04T10:01:09.0258194', '/app/wwwroot/doc1/9aa19c3d-a116-48e7-a3bb-b3d969db5d74.png', '/app/wwwroot/doc2/db7ed22a-efe9-4cc1-9664-ad9a87721474.png', '/app/wwwroot/logo/aeb42b91-8db2-4d02-8ac1-6c3155f46d28.png', 'password');
                SET IDENTITY_INSERT [dbo].[Agencies] OFF;
            ");
        }

        // Seed Users
        if (!context.Users.Any())
        {
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[Users] ON;
                INSERT INTO [Users] ([UserId], [FirstName], [LastName], [Email], [PasswordHash], [CreatedAt], [IsActive])
                VALUES
                    (2, 'Charuka', 'Perera', 'charuka.perera@example.com', 'Test@123', '2025-08-08T12:54:36.6233493', 1),
                    (3, 'Nimesha', 'Fernando', 'nimesha.fernando@example.com', 'Pwd@456', '2025-08-08T12:54:36.6250859', 1),
                    (4, 'Kavindu', 'Jayasinghe', 'kavindu.jayasinghe@example.com', 'Pass@789', '2025-08-08T12:54:36.6250887', 1),
                    (5, 'Tharushi', 'Wijesinghe', 'tharushi.wijesinghe@example.com', 'Hello@321', '2025-08-08T12:54:36.6250893', 1),
                    (6, 'Sahan', 'Bandara', 'sahan.bandara@example.com', 'Secure@987', '2025-08-08T12:54:36.6250896', 1),
                    (7, 'Dilini', 'Silva', 'dilini.silva@example.com', 'Alpha@123', '2025-08-08T12:54:36.6250927', 1),
                    (8, 'Chathura', 'Rathnayake', 'chathura.rathnayake@example.com', 'MyPwd@456', '2025-08-08T12:54:36.6250930', 1),
                    (9, 'Pavithra', 'Herath', 'pavithra.herath@example.com', 'Test@789', '2025-08-08T12:54:36.6250932', 1),
                    (10, 'Isuru', 'Abeysinghe', 'isuru.abeysinghe@example.com', 'Pwd@852', '2025-08-08T12:54:36.6250936', 1),
                    (11, 'Harshani', 'Karunaratne', 'harshani.karunaratne@example.com', 'Pass@741', '2025-08-08T12:54:36.6250941', 1),
                    (12, 'Ravindu', 'Dias', 'ravindu.dias@example.com', 'Qwerty@963', '2025-08-08T12:54:36.6250944', 1),
                    (13, 'Udari', 'Wickramasinghe', 'udari.wickramasinghe@example.com', 'Safe@159', '2025-08-08T12:54:36.6250947', 1),
                    (14, 'Lakshan', 'Gunarathna', 'lakshan.gunarathna@example.com', 'Pwd@753', '2025-08-08T12:54:36.6250959', 1),
                    (15, 'Senuri', 'Gunasekara', 'senuri.gunasekara@example.com', 'Test@951', '2025-08-08T12:54:36.6250962', 1),
                    (16, 'Ashen', 'Ekanayake', 'ashen.ekanayake@example.com', 'Alpha@357', '2025-08-08T12:54:36.6250966', 1),
                    (17, 'Ishara', 'Weerasinghe', 'ishara.weerasinghe@example.com', 'Pass@258', '2025-08-08T12:54:36.6250968', 1),
                    (18, 'Sandun', 'Jayawardena', 'sandun.jayawardena@example.com', 'Hello@147', '2025-08-08T12:54:36.6250972', 1),
                    (19, 'Nadeesha', 'Samarasinghe', 'nadeesha.samarasinghe@example.com', 'Secure@369', '2025-08-08T12:54:36.6250981', 1),
                    (20, 'Pasindu', 'Withanage', 'pasindu.withanage@example.com', 'MyPwd@654', '2025-08-08T12:54:36.6250984', 1),
                    (21, 'Thilini', 'Jayakody', 'thilini.jayakody@example.com', 'Safe@852', '2025-08-08T12:54:36.6250987', 1),
                    (25, 'Charuka', 'Niluminda', 'kvcniluminda@gmail.com', '6230989', '2025-08-09T10:51:02.9494096', 1),
                    (26, 'sunil', 'narine', 'narine@gmail.com', '123456', '2025-08-11T15:46:29.7493297', 1);
                SET IDENTITY_INSERT [dbo].[Users] OFF;
            ");
        }

        // Seed Agency Reviews
        if (!context.AgencyReviews.Any())
        {
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[AgencyReviews] ON;
                INSERT INTO [AgencyReviews] ([ReviewId], [UserId], [AgencyId], [ReviewText], [CreatedAt], [UpdatedAt], [ServiceNumber])
                VALUES
                    (3, 2, 5, 'The process was quick and smooth. I got my offer letter in just two weeks.', '2025-08-08T13:03:39.6581132', '2025-08-08T13:03:39.6581138', 'GMS-2025-001'),
                    (4, 3, 6, 'They were friendly but sometimes slow to respond to emails.', '2025-08-08T13:03:39.6586892', '2025-08-08T13:03:39.6586899', 'EI-2025-002'),
                    (5, 4, 7, 'Very professional handling of my documents. Felt well-guided throughout.', '2025-08-08T13:03:39.6586909', '2025-08-08T13:03:39.6586909', 'CR-2025-003'),
                    (6, 5, 8, 'Had to follow up several times for updates. Could improve communication.', '2025-08-08T13:03:39.6586911', '2025-08-08T13:03:39.6586912', 'OJL-2025-004'),
                    (7, 6, 9, 'Everything was done properly, and I was updated regularly.', '2025-08-08T13:03:39.6586914', '2025-08-08T13:03:39.6586914', 'SOE-2025-005'),
                    (8, 7, 10, 'Helpful staff but the medical check process took longer than expected.', '2025-08-08T13:03:39.6586931', '2025-08-08T13:03:39.6586932', 'LTF-2025-006'),
                    (9, 8, 11, 'The whole procedure was stress-free, and my job placement was exactly as promised.', '2025-08-08T13:03:39.6586934', '2025-08-08T13:03:39.6586935', 'SLM-2025-007'),
                    (10, 9, 12, 'They charged a reasonable fee and delivered on time.', '2025-08-08T13:03:39.6586936', '2025-08-08T13:03:39.6586937', 'EGR-2025-008'),
                    (11, 10, 14, 'A bit of confusion with my flight booking, but they resolved it quickly.', '2025-08-08T13:03:39.6586939', '2025-08-08T13:03:39.6586940', 'TAM-2025-009'),
                    (12, 11, 13, 'The training session before departure was very useful and practical.', '2025-08-08T13:03:39.6586943', '2025-08-08T13:03:39.6586944', 'BOES-2025-010');
                SET IDENTITY_INSERT [dbo].[AgencyReviews] OFF;
            ");
        }

        // Seed AgencyCountries
        if (!context.AgencyCountries.Any())
        {
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[AgencyCountries] ON;
                INSERT INTO [AgencyCountries] ([AgencyCountryId], [AgencyId], [CountryId], [CreatedAt])
                VALUES
                    (45, 5, 1, '2025-08-06T14:01:43.4372668'),
                    (46, 5, 4, '2025-08-06T14:01:43.4374969'),
                    (47, 5, 8, '2025-08-06T14:01:43.4374976'),
                    (48, 5, 10, '2025-08-06T14:01:43.4374978'),
                    (49, 5, 11, '2025-08-06T14:01:43.4374980'),
                    (50, 5, 13, '2025-08-06T14:01:43.4374991'),
                    (51, 5, 20, '2025-08-06T14:01:43.4374993'),
                    (52, 5, 22, '2025-08-06T14:01:43.4374995'),
                    (53, 5, 23, '2025-08-06T14:01:43.4374997'),
                    (54, 6, 1, '2025-08-06T14:01:43.4375000'),
                    (55, 6, 2, '2025-08-06T14:01:43.4375001'),
                    (56, 6, 3, '2025-08-06T14:01:43.4375003'),
                    (57, 6, 9, '2025-08-06T14:01:43.4375005'),
                    (58, 6, 12, '2025-08-06T14:01:43.4375007'),
                    (59, 6, 13, '2025-08-06T14:01:43.4375009'),
                    (60, 6, 15, '2025-08-06T14:01:43.4375010'),
                    (61, 6, 16, '2025-08-06T14:01:43.4375012'),
                    (62, 6, 18, '2025-08-06T14:01:43.4375016'),
                    (63, 6, 25, '2025-08-06T14:01:43.4375017'),
                    (64, 7, 4, '2025-08-06T14:01:43.4375019'),
                    (65, 7, 5, '2025-08-06T14:01:43.4375021'),
                    (66, 7, 6, '2025-08-06T14:01:43.4375022'),
                    (67, 7, 9, '2025-08-06T14:01:43.4375024'),
                    (68, 7, 12, '2025-08-06T14:01:43.4375026'),
                    (69, 7, 14, '2025-08-06T14:01:43.4375028'),
                    (70, 7, 23, '2025-08-06T14:01:43.4375029'),
                    (71, 7, 24, '2025-08-06T14:01:43.4375031'),
                    (72, 8, 1, '2025-08-06T14:01:43.4375033'),
                    (73, 8, 2, '2025-08-06T14:01:43.4375034'),
                    (74, 8, 4, '2025-08-06T14:01:43.4375036'),
                    (75, 8, 7, '2025-08-06T14:01:43.4375038'),
                    (76, 8, 10, '2025-08-06T14:01:43.4375039'),
                    (77, 8, 11, '2025-08-06T14:01:43.4375041'),
                    (78, 8, 17, '2025-08-06T14:01:43.4375045'),
                    (79, 8, 20, '2025-08-06T14:01:43.4375047'),
                    (80, 9, 2, '2025-08-06T14:01:43.4375049'),
                    (81, 9, 6, '2025-08-06T14:01:43.4375050'),
                    (82, 9, 7, '2025-08-06T14:01:43.4375052'),
                    (83, 9, 10, '2025-08-06T14:01:43.4375054'),
                    (84, 9, 13, '2025-08-06T14:01:43.4375055'),
                    (85, 9, 15, '2025-08-06T14:01:43.4375057'),
                    (86, 9, 16, '2025-08-06T14:01:43.4375059'),
                    (87, 9, 24, '2025-08-06T14:01:43.4375061'),
                    (88, 9, 25, '2025-08-06T14:01:43.4375062'),
                    (89, 10, 2, '2025-08-06T14:01:43.4375064'),
                    (90, 10, 3, '2025-08-06T14:01:43.4375065'),
                    (91, 10, 4, '2025-08-06T14:01:43.4375067'),
                    (92, 10, 5, '2025-08-06T14:01:43.4375069'),
                    (93, 10, 6, '2025-08-06T14:01:43.4375070'),
                    (94, 10, 8, '2025-08-06T14:01:43.4375072'),
                    (95, 10, 11, '2025-08-06T14:01:43.4375074'),
                    (96, 10, 12, '2025-08-06T14:01:43.4375075'),
                    (97, 10, 18, '2025-08-06T14:01:43.4375077'),
                    (98, 10, 21, '2025-08-06T14:01:43.4375079'),
                    (99, 11, 1, '2025-08-06T14:01:43.4375081'),
                    (100, 11, 2, '2025-08-06T14:01:43.4375083'),
                    (101, 11, 5, '2025-08-06T14:01:43.4375084'),
                    (102, 11, 8, '2025-08-06T14:01:43.4375086'),
                    (103, 11, 11, '2025-08-06T14:01:43.4375088'),
                    (104, 11, 14, '2025-08-06T14:01:43.4375089'),
                    (105, 11, 16, '2025-08-06T14:01:43.4375091'),
                    (106, 11, 17, '2025-08-06T14:01:43.4375093'),
                    (107, 11, 25, '2025-08-06T14:01:43.4375095'),
                    (108, 12, 4, '2025-08-06T14:01:43.4375097'),
                    (109, 12, 6, '2025-08-06T14:01:43.4375098'),
                    (110, 12, 7, '2025-08-06T14:01:43.4375103'),
                    (111, 12, 9, '2025-08-06T14:01:43.4375104'),
                    (112, 12, 13, '2025-08-06T14:01:43.4375106'),
                    (113, 12, 20, '2025-08-06T14:01:43.4375108'),
                    (114, 12, 22, '2025-08-06T14:01:43.4375110'),
                    (115, 12, 23, '2025-08-06T14:01:43.4375111'),
                    (116, 13, 2, '2025-08-06T14:01:43.4375113'),
                    (117, 13, 3, '2025-08-06T14:01:43.4375115'),
                    (118, 13, 6, '2025-08-06T14:01:43.4375116'),
                    (119, 13, 9, '2025-08-06T14:01:43.4375118'),
                    (120, 13, 12, '2025-08-06T14:01:43.4375120'),
                    (121, 13, 15, '2025-08-06T14:01:43.4375121'),
                    (122, 13, 16, '2025-08-06T14:01:43.4375123'),
                    (123, 13, 18, '2025-08-06T14:01:43.4375124'),
                    (124, 13, 20, '2025-08-06T14:01:43.4375126'),
                    (125, 13, 21, '2025-08-06T14:01:43.4375128'),
                    (126, 14, 5, '2025-08-06T14:01:43.4375129'),
                    (127, 14, 7, '2025-08-06T14:01:43.4375131'),
                    (128, 14, 8, '2025-08-06T14:01:43.4375133'),
                    (129, 14, 10, '2025-08-06T14:01:43.4375135'),
                    (130, 14, 14, '2025-08-06T14:01:43.4375136'),
                    (131, 14, 17, '2025-08-06T14:01:43.4375138'),
                    (132, 14, 21, '2025-08-06T14:01:43.4375139'),
                    (133, 14, 24, '2025-08-06T14:01:43.4375141');
                SET IDENTITY_INSERT [dbo].[AgencyCountries] OFF;
            ");
        }

        // Seed Jobs - Part 1 (Jobs 7-50)
        if (!context.Jobs.Any())
        {
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[Jobs] ON;
                INSERT INTO [Jobs] ([JobId], [JobTitle], [JobDescription], [CategoryId], [AgencyId], [CountryId], [SalaryRange], [Requirements], [IsUrgent], [IsActive], [Deadline], [CreatedAt], [OpenedUrgently], [RegionId])
                VALUES
                    (7, 'Nanny/Housemaid', 'Child supervision, school prep, light cooking/cleaning', 6, 10, 2, 'KWD 100 – 150', 'Childcare experience is mandatory', 1, 1, '2025-08-28T00:00:00.0000000', '2025-08-05T03:08:45.0717468', 0, 1),
                    (8, 'Live-in Caregiver', 'Senior care, light cleaning, medication management, companionship.', 6, 10, 4, 'GBP 1,400 – 1,800', 'Basic English; caregiving certificate', 1, 1, '2025-10-02T00:00:00.0000000', '2025-08-05T03:11:01.9515976', 1, 2),
                    (9, 'Hotel Room Attendant', 'Cleaning guest rooms, changing linens, reporting maintenance issues.', 6, 10, 4, 'EUR 1,600 – 2,000', 'Conversational German is an advantage', 1, 1, '2025-10-02T00:00:00.0000000', '2025-08-05T03:14:10.3031804', 0, 2),
                    (10, 'Residential Cleaner', 'House cleaning tasks: dusting, floors, kitchens, restrooms.', 6, 10, 6, 'EUR 1,500 – 1,800', 'At least 6 months of cleaning exp.', 1, 1, '2025-10-02T00:00:00.0000000', '2025-08-05T03:15:18.4401680', 0, 2),
                    (11, 'Housekeeping Attendant', 'Maintains room cleanliness, restocking, waste disposal in hotels.', 6, 13, 20, '¥170,000 – ¥210,000', 'Basic Japanese language skill', 1, 1, '2025-08-28T00:00:00.0000000', '2025-08-05T03:18:24.0153318', 0, 3),
                    (12, 'Apartment Cleaner', 'Weekly cleaning contracts for homes/apartments.', 6, 13, 21, 'KRW 1.6M – 2.2M', 'Background check; respectful manners', 1, 1, '2025-08-28T00:00:00.0000000', '2025-08-05T03:20:45.0625390', 0, 4),
                    (13, 'Caretaker + Housekeeper', 'Household cleaning, elderly assistance, grocery help.', 6, 13, 16, 'LBP 1.6M – 2.2M', 'Experience with senior care preferred', 1, 1, '2025-08-28T00:00:00.0000000', '2025-08-05T03:22:26.7771181', 0, 1),
                    (14, 'Registered Nurse - ICU', 'Experienced ICU nurse required for leading hospital in Dubai. Must have minimum 3 years ICU experience and valid nursing license. Excellent benefits package including accommodation and health insurance.', 2, 5, 1, '$3,500 - $4,500 USD', 'Bachelor''s degree in Nursing, 3+ years ICU experience, Valid nursing license, English proficiency', 1, 1, '2025-08-25T10:00:00.0000000', '2025-08-05T03:59:31.2033866', 1, 1),
                    (15, 'Civil Engineer - Infrastructure Projects', 'Lead civil engineer needed for major infrastructure development projects in Germany. Responsible for project planning, design supervision, and quality control of construction activities.', 3, 7, 4, '€55,000 - €75,000 EUR', 'Master''s degree in Civil Engineering, 5+ years experience, German language skills preferred, AutoCAD proficiency', 0, 1, '2025-09-15T12:00:00.0000000', '2025-08-05T03:59:31.2040102', 1, 2),
                    (16, 'Hotel Manager - Luxury Resort', 'Seeking experienced hotel manager for 5-star resort in Kuwait. Oversee daily operations, staff management, guest services, and revenue optimization. International hospitality experience preferred.', 4, 9, 2, '$4,000 - $6,000 USD', 'Bachelor''s degree in Hospitality Management, 7+ years management experience, Fluent English and Arabic', 1, 1, '2025-08-20T14:30:00.0000000', '2025-08-05T03:59:31.2040122', 1, 1),
                    (17, 'Logistics Coordinator - Warehouse Operations', 'Coordinate logistics operations for major distribution center in Netherlands. Manage inventory, shipping schedules, and supplier relationships. Experience with SAP systems required.', 5, 11, 5, '€45,000 - €55,000 EUR', 'Bachelor''s degree in Logistics/Supply Chain, 3+ years experience, SAP knowledge, Dutch language basic level', 0, 1, '2025-09-01T09:00:00.0000000', '2025-08-05T03:59:31.2040125', 0, 2),
                    (18, 'Housekeeping Supervisor', 'Lead housekeeping team for luxury hotel chain in Ireland. Ensure highest standards of cleanliness, train staff, manage schedules and inventory. Previous supervisory experience essential.', 6, 13, 6, '€35,000 - €42,000 EUR', 'High school diploma, 3+ years housekeeping experience, 2+ years supervisory role, English fluency', 0, 1, '2025-08-30T16:00:00.0000000', '2025-08-05T03:59:31.2040127', 0, 2);
                SET IDENTITY_INSERT [dbo].[Jobs] OFF;
            ");

            // Continue with Part 2 of Jobs data
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[Jobs] ON;
                INSERT INTO [Jobs] ([JobId], [JobTitle], [JobDescription], [CategoryId], [AgencyId], [CountryId], [SalaryRange], [Requirements], [IsUrgent], [IsActive], [Deadline], [CreatedAt], [OpenedUrgently], [RegionId])
                VALUES
                    (19, 'Cardiac Surgeon', 'Senior cardiac surgeon position available at prestigious medical center in Saudi Arabia. Perform complex cardiac procedures, lead surgical team, contribute to medical research and education.', 2, 6, 3, '$12,000 - $18,000 USD', 'MD degree, Board certification in Cardiac Surgery, 10+ years experience, Medical license, Arabic language advantage', 1, 1, '2025-08-18T11:00:00.0000000', '2025-08-05T03:59:31.2040141', 0, 1),
                    (20, 'Construction Project Manager', 'Manage large-scale construction project in Sweden. Oversee project timeline, budget, safety compliance, and contractor coordination. Green building certification experience preferred.', 3, 8, 7, 'SEK 650,000 - 800,000', 'Construction Management degree, PMP certification, 8+ years experience, Swedish language skills, Safety certifications', 0, 1, '2025-09-10T13:00:00.0000000', '2025-08-05T03:59:31.2040145', 0, 2),
                    (21, 'Restaurant Chef - Fine Dining', 'Executive chef position at Michelin-starred restaurant in Denmark. Create innovative menus, manage kitchen operations, train culinary staff, maintain highest food quality standards.', 4, 10, 8, 'DKK 480,000 - 620,000', 'Culinary Arts degree, 8+ years fine dining experience, Michelin restaurant background preferred, Danish language basic', 1, 1, '2025-08-22T15:30:00.0000000', '2025-08-05T03:59:31.2040148', 0, 2),
                    (22, 'Supply Chain Analyst', 'Analyze supply chain operations for multinational corporation in Norway. Optimize logistics processes, reduce costs, improve efficiency using data analytics and forecasting models.', 5, 12, 9, 'NOK 550,000 - 700,000', 'Supply Chain Management degree, 4+ years analysis experience, Excel/SQL proficiency, Norwegian language advantage', 0, 1, '2025-09-05T10:30:00.0000000', '2025-08-05T03:59:31.2040155', 0, 2),
                    (23, 'Residential Cleaner', 'Professional residential cleaning services in Finland. Provide thorough cleaning for luxury homes and apartments. Flexible schedule, eco-friendly products, attention to detail required.', 6, 14, 10, '€28,000 - €35,000 EUR', 'Previous cleaning experience, Reliability and trustworthiness, Finnish language basic level, Own transportation preferred', 0, 1, '2025-08-28T08:00:00.0000000', '2025-08-05T03:59:31.2040160', 0, 2),
                    (24, 'Physical Therapist', 'Licensed physical therapist for rehabilitation center in Poland. Treat patients with musculoskeletal injuries, develop treatment plans, use modern rehabilitation equipment and techniques.', 2, 5, 11, 'PLN 120,000 - 150,000', 'Physical Therapy degree, Valid PT license, 3+ years experience, Polish language skills, Manual therapy certification', 1, 1, '2025-08-19T12:00:00.0000000', '2025-08-05T03:59:31.2040163', 0, 2),
                    (25, 'Electrical Engineer - Power Systems', 'Design and maintain electrical power systems for industrial projects in Qatar. Work on power distribution, control systems, and renewable energy integration projects.', 3, 7, 12, '$5,500 - $7,500 USD', 'Electrical Engineering degree, 6+ years power systems experience, Professional Engineer license, Arabic language advantage', 1, 1, '2025-08-16T14:00:00.0000000', '2025-08-05T03:59:31.2040166', 0, 1),
                    (26, 'Banquet Manager', 'Manage banquet operations for luxury hotel in Bahrain. Coordinate events, supervise banquet staff, ensure exceptional service delivery for weddings, conferences, and special occasions.', 4, 9, 13, '$3,800 - $5,200 USD', 'Hospitality Management degree, 5+ years banquet experience, Event planning skills, English and Arabic fluency', 0, 1, '2025-09-12T11:30:00.0000000', '2025-08-05T03:59:31.2040168', 0, 1),
                    (27, 'Freight Operations Manager', 'Oversee freight operations for shipping company in Yemen. Manage cargo handling, documentation, customs clearance, and transportation logistics for international shipments.', 5, 11, 14, '$2,800 - $4,000 USD', 'Logistics degree, 6+ years freight experience, Customs procedures knowledge, Arabic and English fluency', 1, 1, '2025-08-24T13:00:00.0000000', '2025-08-05T03:59:31.2040170', 0, 1),
                    (28, 'Domestic Helper - Elder Care', 'Provide compassionate care for elderly clients in Jordan. Assist with daily activities, light housekeeping, meal preparation, and companionship. Previous elder care experience required.', 6, 13, 15, '$800 - $1,200 USD', 'Elder care experience, First Aid certification, Patience and compassion, Arabic language skills, Health certificate', 0, 1, '2025-08-27T09:30:00.0000000', '2025-08-05T03:59:31.2040174', 0, 1),
                    (29, 'Radiologist', 'Diagnostic radiologist position at modern medical facility in Lebanon. Interpret medical imaging, provide accurate diagnoses, collaborate with medical team, use advanced imaging technology.', 2, 6, 16, '$6,000 - $9,000 USD', 'Medical degree, Radiology residency, Board certification, 5+ years experience, Arabic and English proficiency', 1, 1, '2025-08-21T10:15:00.0000000', '2025-08-05T03:59:31.2040176', 0, 1),
                    (30, 'Site Supervisor - Commercial Building', 'Supervise construction site for commercial building project in Syria. Ensure safety compliance, quality control, coordinate subcontractors, maintain project schedules and documentation.', 3, 8, 17, '$2,500 - $3,800 USD', 'Construction Engineering degree, 5+ years site supervision, Safety certifications, Arabic language fluency, Leadership skills', 0, 1, '2025-09-08T14:30:00.0000000', '2025-08-05T03:59:31.2040179', 0, 1);
                SET IDENTITY_INSERT [dbo].[Jobs] OFF;
            ");

            // Continue with remaining jobs (31-98) in a third batch
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[Jobs] ON;
                INSERT INTO [Jobs] ([JobId], [JobTitle], [JobDescription], [CategoryId], [AgencyId], [CountryId], [SalaryRange], [Requirements], [IsUrgent], [IsActive], [Deadline], [CreatedAt], [OpenedUrgently], [RegionId])
                VALUES
                    (31, 'Hotel Receptionist', 'Front desk receptionist for international hotel in Iraq. Handle guest check-in/out, reservations, concierge services, and customer inquiries. Excellent communication skills essential.', 4, 10, 18, '$1,800 - $2,500 USD', 'High school diploma, 2+ years hotel experience, Computer literacy, Arabic and English fluency, Customer service skills', 0, 1, '2025-09-03T12:45:00.0000000', '2025-08-05T03:59:31.2040183', 0, 1),
                    (32, 'Warehouse Manager', 'Manage warehouse operations for electronics distribution company in Japan. Oversee inventory management, staff supervision, quality control, and optimize warehouse efficiency using modern systems.', 5, 12, 20, '¥4,500,000 - ¥6,200,000 JPY', 'Supply Chain Management degree, 7+ years warehouse experience, Japanese language skills, WMS system knowledge', 1, 1, '2025-08-17T16:00:00.0000000', '2025-08-05T03:59:31.2040186', 0, 3),
                    (33, 'Nanny - Child Care', 'Experienced nanny for expatriate family in South Korea. Provide full-time childcare for two children, educational activities, meal preparation, light housekeeping, and transportation.', 6, 14, 21, '₩35,000,000 - ₩45,000,000 KRW', 'Childcare certification, 4+ years nanny experience, English fluency, Korean language basic, Clean driving record', 1, 1, '2025-08-23T11:15:00.0000000', '2025-08-05T03:59:31.2040189', 0, 4),
                    (34, 'Medical Laboratory Technician', 'Laboratory technician position at leading medical laboratory in Taiwan. Perform clinical tests, operate laboratory equipment, maintain quality standards, assist in research projects.', 2, 5, 22, 'NT$600,000 - NT$750,000 TWD', 'Medical Technology degree, Laboratory certification, 3+ years experience, Mandarin and English proficiency, Attention to detail', 0, 1, '2025-09-07T13:30:00.0000000', '2025-08-05T03:59:31.2040191', 0, 3),
                    (35, 'Architecture Designer', 'Architectural designer for prestigious design firm in China. Create innovative building designs, prepare technical drawings, collaborate with engineering teams, participate in project presentations.', 3, 7, 23, '¥280,000 - ¥380,000 CNY', 'Architecture degree, 4+ years design experience, AutoCAD/Revit proficiency, Mandarin language skills, Portfolio required', 0, 1, '2025-09-14T15:00:00.0000000', '2025-08-05T03:59:31.2040194', 1, 3),
                    (36, 'Casino Dealer', 'Professional casino dealer for luxury casino resort in Macau. Deal various table games, provide excellent customer service, maintain game integrity, work in fast-paced environment.', 4, 9, 24, 'HK$25,000 - HK$35,000 HKD', 'Dealer certification, 2+ years casino experience, Cantonese and English fluency, Professional appearance, Mathematical skills', 1, 1, '2025-08-26T17:00:00.0000000', '2025-08-05T03:59:31.2040198', 0, 3),
                    (37, 'Import/Export Coordinator', 'Coordinate international trade operations for trading company in Hong Kong. Handle documentation, customs clearance, shipping arrangements, and maintain client relationships.', 5, 11, 25, 'HK$35,000 - HK$45,000 HKD', 'International Trade degree, 3+ years import/export experience, Customs procedures knowledge, Cantonese and English fluency', 0, 1, '2025-09-02T14:45:00.0000000', '2025-08-05T03:59:31.2040200', 0, 3),
                    (38, 'Pediatric Nurse', 'Specialized pediatric nurse for children''s hospital in Dubai. Provide nursing care for pediatric patients, support families, administer medications, maintain patient records.', 2, 6, 1, '$3,200 - $4,200 USD', 'Nursing degree, Pediatric nursing certification, 3+ years pediatric experience, English proficiency, Compassionate nature', 1, 1, '2025-08-20T09:00:00.0000000', '2025-08-05T03:59:31.2040288', 0, 1),
                    (39, 'MEP Engineer', 'Mechanical, Electrical, and Plumbing engineer for construction projects in Kuwait. Design MEP systems, review technical drawings, ensure code compliance, supervise installations.', 3, 8, 2, '$4,500 - $6,500 USD', 'MEP Engineering degree, 5+ years MEP experience, AutoCAD proficiency, Arabic language advantage, Professional Engineer license', 0, 1, '2025-09-11T12:30:00.0000000', '2025-08-05T03:59:31.2040291', 0, 1),
                    (40, 'Spa Therapist', 'Professional spa therapist for luxury wellness resort in Saudi Arabia. Provide massage therapy, facial treatments, body treatments, maintain treatment room hygiene, ensure client satisfaction.', 4, 10, 3, '$2,800 - $3,800 USD', 'Spa therapy certification, 3+ years spa experience, Massage therapy license, English proficiency, Professional demeanor', 0, 1, '2025-08-29T16:30:00.0000000', '2025-08-05T03:59:31.2040293', 0, 1),
                    (41, 'Transportation Coordinator', 'Coordinate transportation services for logistics company in Germany. Manage vehicle schedules, route optimization, driver assignments, maintain delivery standards and customer satisfaction.', 5, 12, 4, '€42,000 - €52,000 EUR', 'Transportation Management degree, 4+ years coordination experience, German language skills, GPS systems knowledge', 1, 1, '2025-08-15T11:45:00.0000000', '2025-08-05T03:59:31.2040296', 0, 2),
                    (42, 'Office Cleaner', 'Professional office cleaning services for corporate buildings in Netherlands. Maintain cleanliness of offices, restrooms, common areas, handle waste management, use eco-friendly products.', 6, 14, 5, '€25,000 - €30,000 EUR', 'Previous cleaning experience, Reliability and punctuality, Dutch language basic level, Physical fitness, Attention to detail', 0, 1, '2025-09-04T08:30:00.0000000', '2025-08-05T03:59:31.2040299', 0, 2),
                    (43, 'Emergency Room Physician', 'Emergency medicine physician for busy trauma center in Ireland. Provide critical care, diagnose emergency conditions, manage trauma cases, work in high-pressure environment with multidisciplinary team.', 2, 7, 6, '€95,000 - €125,000 EUR', 'Medical degree, Emergency Medicine residency, Board certification, 5+ years ER experience, BLS/ACLS certification', 1, 1, '2025-08-18T14:00:00.0000000', '2025-08-05T03:59:31.2040301', 0, 2),
                    (44, 'Safety Engineer', 'Occupational safety engineer for offshore oil platform construction in Sweden. Develop safety protocols, conduct risk assessments, ensure regulatory compliance, train personnel on safety procedures.', 3, 9, 7, 'SEK 580,000 - 720,000', 'Safety Engineering degree, 6+ years offshore safety experience, NEBOSH certification, Swedish language skills, HSE knowledge', 0, 1, '2025-09-20T10:00:00.0000000', '2025-08-05T03:59:31.2040304', 0, 2),
                    (45, 'Food & Beverage Manager', 'Oversee all food and beverage operations for luxury cruise ship in Denmark. Manage multiple restaurants, bars, room service, control costs, ensure quality standards, supervise large team.', 4, 11, 8, 'DKK 420,000 - 550,000', 'Hospitality Management degree, 8+ years F&B management, Cruise experience preferred, Danish language advantage, Leadership skills', 1, 1, '2025-08-22T16:45:00.0000000', '2025-08-05T03:59:31.2040312', 0, 2),
                    (46, 'Procurement Specialist', 'Strategic procurement specialist for manufacturing company in Norway. Source materials, negotiate contracts, manage supplier relationships, optimize costs while maintaining quality standards.', 5, 13, 9, 'NOK 480,000 - 620,000', 'Supply Chain/Business degree, 5+ years procurement experience, Contract negotiation skills, Norwegian language proficiency, SAP knowledge', 0, 1, '2025-09-18T11:30:00.0000000', '2025-08-05T03:59:31.2040314', 0, 2),
                    (47, 'Gardener - Estate Maintenance', 'Professional gardener for luxury private estate in Finland. Maintain extensive gardens, lawns, greenhouse operations, seasonal plantings, irrigation systems, outdoor recreational areas.', 6, 5, 10, '€32,000 - €38,000 EUR', 'Horticulture certificate, 4+ years landscaping experience, Plant disease knowledge, Finnish language basic, Physical fitness', 0, 1, '2025-08-31T09:15:00.0000000', '2025-08-05T03:59:31.2040319', 0, 2),
                    (48, 'Anesthesiologist', 'Staff anesthesiologist for major medical center in Poland. Provide anesthesia services for surgeries, monitor patients during procedures, manage pain control, work with surgical teams.', 2, 8, 11, 'PLN 180,000 - 250,000', 'Medical degree, Anesthesiology residency, Board certification, 4+ years experience, Polish medical license, BLS certification', 1, 1, '2025-08-25T13:20:00.0000000', '2025-08-05T03:59:31.2040322', 0, 2),
                    (49, 'Structural Engineer', 'Design structural systems for high-rise buildings in Qatar. Perform structural analysis, prepare engineering drawings, ensure seismic and wind load compliance, coordinate with architects.', 3, 10, 12, '$6,200 - $8,500 USD', 'Structural Engineering degree, 7+ years high-rise experience, ETABS/SAP2000 proficiency, Professional Engineer license, Arabic advantage', 0, 1, '2025-09-25T14:45:00.0000000', '2025-08-05T03:59:31.2040324', 0, 1),
                    (50, 'Event Coordinator', 'Plan and execute corporate events and conferences in Bahrain. Coordinate venues, vendors, logistics, manage budgets, ensure seamless event delivery, handle client relationships.', 4, 12, 13, '$3,200 - $4,500 USD', 'Event Management degree, 4+ years event planning, Vendor management skills, Arabic and English fluency, Creative thinking', 0, 1, '2025-09-10T15:30:00.0000000', '2025-08-05T03:59:31.2040327', 0, 1);
                SET IDENTITY_INSERT [dbo].[Jobs] OFF;
            ");

            // Continue with jobs 51-98 in final batch
            await context.Database.ExecuteSqlRawAsync(@"
                SET IDENTITY_INSERT [dbo].[Jobs] ON;
                INSERT INTO [Jobs] ([JobId], [JobTitle], [JobDescription], [CategoryId], [AgencyId], [CountryId], [SalaryRange], [Requirements], [IsUrgent], [IsActive], [Deadline], [CreatedAt], [OpenedUrgently], [RegionId])
                VALUES
                    (51, 'Inventory Control Manager', 'Manage inventory operations for pharmaceutical distribution in Yemen. Control stock levels, implement inventory systems, reduce waste, ensure regulatory compliance, manage warehouse team.', 5, 14, 14, '$3,500 - $4,800 USD', 'Supply Chain Management degree, 6+ years inventory management, Pharmaceutical experience preferred, Arabic fluency, ERP system knowledge', 1, 1, '2025-08-28T12:00:00.0000000', '2025-08-05T03:59:31.2040330', 0, 1),
                    (52, 'Personal Chef', 'Private chef for high-profile family in Jordan. Plan menus, prepare gourmet meals, accommodate dietary restrictions, manage kitchen inventory, maintain highest culinary standards.', 6, 6, 15, '$2,200 - $3,200 USD', 'Culinary Arts degree, 5+ years private chef experience, International cuisine expertise, Arabic language skills, Discretion and professionalism', 0, 1, '2025-09-05T10:45:00.0000000', '2025-08-05T03:59:31.2040332', 0, 1),
                    (53, 'Orthopedic Surgeon', 'Orthopedic surgeon specializing in sports medicine at leading hospital in Lebanon. Perform surgeries, treat sports injuries, provide rehabilitation guidance, work with athletic teams.', 2, 9, 16, '$8,500 - $12,000 USD', 'Medical degree, Orthopedic Surgery residency, Sports Medicine fellowship, 8+ years experience, Arabic and English proficiency', 1, 1, '2025-08-30T11:00:00.0000000', '2025-08-05T03:59:31.2040335', 0, 1),
                    (54, 'Quantity Surveyor', 'Cost estimation and project management for residential construction projects in Syria. Prepare cost estimates, monitor project budgets, handle contract administration, ensure cost control.', 3, 11, 17, '$2,800 - $4,200 USD', 'Quantity Surveying degree, 5+ years construction costing, Contract management skills, Arabic language fluency, AutoCAD knowledge', 0, 1, '2025-09-22T13:15:00.0000000', '2025-08-05T03:59:31.2040337', 1, 1),
                    (55, 'Tour Guide', 'Professional tour guide for cultural and historical tours in Iraq. Lead tourist groups, provide historical information, ensure safety, coordinate with travel agencies, handle multilingual groups.', 4, 13, 18, '$1,500 - $2,200 USD', 'Tourism/History degree, 3+ years tour guide experience, Cultural knowledge, Arabic, English, and additional language skills, First Aid certification', 0, 1, '2025-09-12T09:30:00.0000000', '2025-08-05T03:59:31.2040340', 0, 1),
                    (56, 'Quality Control Manager', 'Oversee quality control operations for automotive parts manufacturing in Japan. Implement QC procedures, manage inspection teams, ensure ISO compliance, reduce defect rates.', 5, 5, 20, '¥5,200,000 - ¥6,800,000 JPY', 'Industrial Engineering degree, 8+ years QC management, Six Sigma certification, Japanese language skills, Automotive industry experience', 1, 1, '2025-08-26T14:20:00.0000000', '2025-08-05T03:59:31.2040342', 0, 3),
                    (57, 'Housekeeper - Luxury Villa', 'Executive housekeeper for luxury private villa in South Korea. Maintain impeccable cleanliness standards, manage household staff, coordinate with property managers, handle valuable items.', 6, 7, 21, '₩42,000,000 - ₩55,000,000 KRW', 'Hospitality/Housekeeping certification, 6+ years luxury housekeeping, Supervisory experience, Korean language skills, Trustworthiness', 0, 1, '2025-09-08T16:00:00.0000000', '2025-08-05T03:59:31.2040345', 0, 4),
                    (58, 'Pharmacist', 'Clinical pharmacist for hospital pharmacy in Taiwan. Dispense medications, provide drug consultations, monitor therapy outcomes, collaborate with medical teams, ensure medication safety.', 2, 10, 22, 'NT$900,000 - NT$1,200,000 TWD', 'Pharmacy degree, Licensed pharmacist, 4+ years clinical experience, Mandarin and English proficiency, Hospital experience preferred', 0, 1, '2025-09-15T12:30:00.0000000', '2025-08-05T03:59:31.2040348', 0, 3),
                    (59, 'Interior Designer', 'Commercial interior designer for office and retail spaces in China. Create functional designs, select materials and furnishings, coordinate with contractors, manage multiple projects simultaneously.', 3, 12, 23, '¥320,000 - ¥450,000 CNY', 'Interior Design degree, 5+ years commercial design, AutoCAD/3D modeling skills, Mandarin language skills, Portfolio required', 0, 1, '2025-09-28T11:45:00.0000000', '2025-08-05T03:59:31.2040350', 0, 3),
                    (60, 'Gaming Floor Supervisor', 'Supervise casino gaming floor operations in Macau. Monitor games, resolve disputes, ensure regulatory compliance, manage dealer performance, provide customer service to VIP clients.', 4, 14, 24, 'HK$32,000 - HK$42,000 HKD', 'Gaming supervision certification, 4+ years casino experience, Cantonese and English fluency, Conflict resolution skills, Mathematical aptitude', 1, 1, '2025-08-29T18:00:00.0000000', '2025-08-05T03:59:31.2040354', 0, 3),
                    (61, 'Customs Broker', 'Handle customs clearance procedures for international shipping company in Hong Kong. Process import/export documentation, ensure regulatory compliance, liaise with customs authorities, manage client accounts.', 5, 6, 25, 'HK$38,000 - HK$52,000 HKD', 'International Trade degree, Customs broker license, 4+ years customs experience, Cantonese and English fluency, Attention to detail', 0, 1, '2025-09-16T13:30:00.0000000', '2025-08-05T03:59:31.2040356', 0, 3),
                    (62, 'Dental Hygienist', 'Dental hygienist for modern dental clinic in Dubai. Perform teeth cleaning, take X-rays, educate patients on oral health, assist dentists with procedures, maintain equipment sterilization.', 2, 8, 1, '$2,800 - $3,800 USD', 'Dental Hygiene degree, Valid license, 2+ years experience, English proficiency, Patient care skills', 0, 1, '2025-09-06T10:15:00.0000000', '2025-08-05T03:59:31.2040358', 0, 1),
                    (63, 'HVAC Technician', 'Install and maintain heating, ventilation, and air conditioning systems for commercial buildings in Kuwait. Troubleshoot system problems, perform routine maintenance, ensure energy efficiency.', 3, 11, 2, '$3,200 - $4,500 USD', 'HVAC certification, 4+ years commercial HVAC experience, Troubleshooting skills, Arabic language advantage, Safety training', 1, 1, '2025-08-21T14:45:00.0000000', '2025-08-05T03:59:31.2040361', 0, 1),
                    (64, 'Wedding Planner', 'Plan and coordinate luxury weddings in Saudi Arabia. Manage all aspects of wedding planning, coordinate with vendors, handle logistics, ensure flawless execution of events.', 4, 13, 3, '$3,500 - $5,000 USD', 'Event Planning certification, 5+ years wedding planning, Vendor management skills, Arabic and English fluency, Creative vision', 0, 1, '2025-09-14T12:00:00.0000000', '2025-08-05T03:59:31.2040364', 0, 1),
                    (65, 'Fleet Manager', 'Manage commercial vehicle fleet for delivery company in Germany. Oversee vehicle maintenance, driver scheduling, route optimization, cost control, ensure regulatory compliance.', 5, 5, 4, '€48,000 - €62,000 EUR', 'Transportation Management degree, 6+ years fleet management, German commercial driving knowledge, GPS tracking systems, Leadership skills', 0, 1, '2025-09-23T15:20:00.0000000', '2025-08-05T03:59:31.2040366', 0, 2),
                    (66, 'Butler - Private Estate', 'Professional butler for luxury private estate in Netherlands. Manage household staff, coordinate events, handle guest services, maintain household inventory, ensure seamless operations.', 6, 7, 5, '€45,000 - €58,000 EUR', 'Butler training certification, 7+ years private service, Wine knowledge, Dutch and English fluency, Discretion and professionalism', 1, 1, '2025-08-27T11:30:00.0000000', '2025-08-05T03:59:31.2040369', 0, 2),
                    (67, 'Occupational Therapist', 'Help patients develop daily living skills at rehabilitation hospital in Ireland. Assess patient needs, create treatment plans, use therapeutic activities, document progress, work with multidisciplinary team.', 2, 9, 6, '€42,000 - €52,000 EUR', 'Occupational Therapy degree, Valid OT license, 3+ years experience, English fluency, Patient assessment skills', 0, 1, '2025-09-11T09:45:00.0000000', '2025-08-05T03:59:31.2040372', 0, 2),
                    (68, 'Marine Engineer', 'Design and maintain marine propulsion systems for shipyard in Sweden. Work on engine installations, conduct sea trials, troubleshoot mechanical problems, ensure maritime safety standards.', 3, 12, 7, 'SEK 620,000 - 780,000', 'Marine Engineering degree, 6+ years shipyard experience, Maritime certifications, Swedish language skills, CAD software proficiency', 1, 1, '2025-08-19T16:30:00.0000000', '2025-08-05T03:59:31.2040375', 0, 2),
                    (69, 'Sommelier', 'Wine sommelier for fine dining restaurant in Denmark. Curate wine selection, provide wine pairings, train service staff on wines, manage wine inventory, ensure proper storage.', 4, 14, 8, 'DKK 380,000 - 480,000', 'Sommelier certification, 4+ years wine service, Extensive wine knowledge, Danish language advantage, Sales skills', 0, 1, '2025-09-17T14:15:00.0000000', '2025-08-05T03:59:31.2040378', 0, 2),
                    (70, 'Distribution Manager', 'Oversee product distribution network for consumer goods company in Norway. Manage distribution centers, optimize delivery routes, control costs, ensure timely product availability.', 5, 6, 9, 'NOK 580,000 - 750,000', 'Supply Chain Management degree, 8+ years distribution management, Norwegian language proficiency, WMS experience, Leadership skills', 0, 1, '2025-09-30T10:00:00.0000000', '2025-08-05T03:59:31.2040379', 0, 2),
                    (71, 'Laundry Supervisor', 'Supervise commercial laundry operations for hotel chain in Finland. Manage laundry staff, ensure quality standards, maintain equipment, control chemical usage, optimize workflow efficiency.', 6, 8, 10, '€33,000 - €40,000 EUR', 'Textile care knowledge, 4+ years laundry supervision, Finnish language skills, Equipment maintenance skills, Quality control experience', 0, 1, '2025-09-07T13:45:00.0000000', '2025-08-05T03:59:31.2040382', 0, 2),
                    (72, 'Speech Therapist', 'Provide speech and language therapy services at pediatric clinic in Poland. Assess communication disorders, develop treatment plans, work with children and families, document progress.', 2, 10, 11, 'PLN 95,000 - 130,000', 'Speech Therapy degree, Valid SLP license, 3+ years pediatric experience, Polish language skills, Patience with children', 1, 1, '2025-08-24T12:20:00.0000000', '2025-08-05T03:59:31.2040389', 0, 2),
                    (73, 'Geotechnical Engineer', 'Conduct soil analysis and foundation design for construction projects in Qatar. Perform site investigations, analyze soil properties, design foundations, ensure structural stability.', 3, 13, 12, '$5,800 - $7,800 USD', 'Geotechnical Engineering degree, 5+ years foundation design, Soil testing experience, Arabic language advantage, Professional Engineer license', 0, 1, '2025-09-26T11:00:00.0000000', '2025-08-05T03:59:31.2040392', 0, 1),
                    (74, 'Cruise Director', 'Entertainment director for luxury cruise line in Bahrain. Plan and coordinate onboard activities, manage entertainment staff, ensure guest satisfaction, handle special events.', 4, 5, 13, '$4,200 - $6,000 USD', 'Entertainment/Hospitality degree, 6+ years cruise experience, Performance skills, Arabic and English fluency, Leadership abilities', 1, 1, '2025-08-23T17:30:00.0000000', '2025-08-05T03:59:31.2040394', 0, 1),
                    (75, 'Port Operations Manager', 'Manage port operations for shipping terminal in Yemen. Coordinate vessel scheduling, cargo handling, equipment maintenance, ensure safety protocols, manage operational staff.', 5, 7, 14, '$4,500 - $6,200 USD', 'Maritime Management degree, 8+ years port operations, Safety certifications, Arabic fluency, Terminal operations experience', 1, 1, '2025-08-20T15:45:00.0000000', '2025-08-05T03:59:31.2040397', 0, 1),
                    (76, 'Driver - Executive Transport', 'Professional chauffeur for executive transportation services in Jordan. Provide safe, reliable transportation for VIP clients, maintain vehicle condition, ensure punctuality and discretion.', 6, 9, 15, '$1,200 - $1,800 USD', 'Professional driving license, 5+ years chauffeur experience, Clean driving record, Arabic and English skills, Professional appearance', 0, 1, '2025-09-04T10:30:00.0000000', '2025-08-05T03:59:31.2040400', 0, 1),
                    (77, 'Neurologist', 'Neurologist specializing in stroke care at medical center in Lebanon. Diagnose neurological conditions, provide treatment plans, conduct research, teach medical students.', 2, 11, 16, '$9,500 - $13,500 USD', 'Medical degree, Neurology residency, Board certification, 6+ years experience, Arabic and English proficiency, Research experience', 1, 1, '2025-08-22T13:00:00.0000000', '2025-08-05T03:59:31.2040403', 0, 1),
                    (78, 'Plumbing Supervisor', 'Supervise plumbing installations for residential construction projects in Syria. Coordinate plumbing crews, ensure code compliance, quality control, manage material inventory.', 3, 14, 17, '$2,200 - $3,500 USD', 'Plumbing trade certification, 6+ years plumbing experience, Supervisory skills, Arabic language fluency, Blueprint reading', 0, 1, '2025-09-19T14:00:00.0000000', '2025-08-05T03:59:31.2040405', 0, 1),
                    (79, 'Travel Agent', 'Plan and book travel arrangements for corporate and leisure clients in Iraq. Handle reservations, create itineraries, provide travel advice, manage client relationships.', 4, 6, 18, '$1,600 - $2,400 USD', 'Tourism/Business degree, 3+ years travel industry, GDS system knowledge, Arabic and English fluency, Customer service skills', 0, 1, '2025-09-13T11:15:00.0000000', '2025-08-05T03:59:31.2040445', 0, 1),
                    (80, 'Operations Research Analyst', 'Optimize business operations using mathematical analysis for manufacturing company in Japan. Analyze data, develop models, improve efficiency, reduce costs, present findings to management.', 5, 8, 20, '¥5,800,000 - ¥7,200,000 JPY', 'Operations Research/Mathematics degree, 5+ years analysis experience, Statistical software proficiency, Japanese language skills, Problem-solving abilities', 0, 1, '2025-09-24T16:20:00.0000000', '2025-08-05T03:59:31.2040448', 0, 3),
                    (81, 'Personal Trainer', 'Provide personal fitness training services for high-end fitness center in South Korea. Design workout programs, provide nutrition guidance, motivate clients, track progress.', 6, 10, 21, '₩38,000,000 - ₩48,000,000 KRW', 'Personal Training certification, 4+ years training experience, Fitness knowledge, Korean language skills, Motivational skills', 0, 1, '2025-09-09T08:45:00.0000000', '2025-08-05T03:59:31.2040449', 0, 4),
                    (82, 'Psychiatrist', 'Provide mental health services at psychiatric hospital in Taiwan. Diagnose mental health conditions, prescribe medications, conduct therapy sessions, collaborate with treatment team.', 2, 12, 22, 'NT$1,400,000 - NT$1,800,000 TWD', 'Medical degree, Psychiatry residency, Board certification, 5+ years experience, Mandarin and English proficiency, Therapy skills', 1, 1, '2025-08-28T14:30:00.0000000', '2025-08-05T03:59:31.2040452', 0, 3),
                    (83, 'Urban Planner', 'Develop city planning projects for municipal government in China. Design urban layouts, analyze population trends, coordinate with stakeholders, ensure sustainable development.', 3, 5, 23, '¥250,000 - ¥350,000 CNY', 'Urban Planning degree, 6+ years municipal planning, GIS software proficiency, Mandarin language skills, Government experience', 0, 1, '2025-10-01T12:00:00.0000000', '2025-08-05T03:59:31.2040455', 0, 3),
                    (84, 'VIP Host', 'Provide personalized service to VIP casino guests in Macau. Assist with gaming needs, arrange accommodations, handle special requests, build client relationships, ensure exceptional experience.', 4, 7, 24, 'HK$28,000 - HK$38,000 HKD', 'Hospitality degree, 3+ years VIP service, Cantonese and English fluency, Customer relationship skills, Professional appearance', 0, 1, '2025-09-21T15:00:00.0000000', '2025-08-05T03:59:31.2040457', 0, 3),
                    (85, 'Shipping Coordinator', 'Coordinate international shipping operations for trading company in Hong Kong. Handle booking, documentation, track shipments, liaise with carriers, ensure timely delivery.', 5, 9, 25, 'HK$32,000 - HK$42,000 HKD', 'Logistics degree, 3+ years shipping coordination, International trade knowledge, Cantonese and English fluency, Detail-oriented', 1, 1, '2025-08-25T13:45:00.0000000', '2025-08-05T03:59:31.2040460', 0, 3),
                    (86, 'Veterinarian', 'Small animal veterinarian for modern veterinary clinic in Dubai. Provide medical care for pets, perform surgeries, educate pet owners, maintain medical records.', 2, 11, 1, '$4,500 - $6,500 USD', 'Veterinary Medicine degree, Valid veterinary license, 4+ years small animal practice, English proficiency, Surgical skills', 0, 1, '2025-09-18T10:00:00.0000000', '2025-08-05T03:59:31.2040463', 0, 1),
                    (87, 'Welder - Structural Steel', 'Certified welder for structural steel construction projects in Kuwait. Perform welding operations, read blueprints, ensure weld quality, maintain safety standards, work at heights.', 3, 13, 2, '$2,800 - $4,200 USD', 'Welding certification, 5+ years structural welding, Blueprint reading, Arabic language advantage, Safety training', 1, 1, '2025-08-17T16:15:00.0000000', '2025-08-05T03:59:31.2040471', 0, 1),
                    (88, 'Conference Manager', 'Manage international conferences and business events in Saudi Arabia. Coordinate logistics, manage vendors, handle registrations, ensure smooth event execution.', 4, 6, 3, '$4,000 - $5,800 USD', 'Event Management degree, 6+ years conference management, Vendor coordination skills, Arabic and English fluency, Project management', 0, 1, '2025-09-27T14:20:00.0000000', '2025-08-05T03:59:31.2040474', 0, 1),
                    (89, 'Demand Planner', 'Forecast product demand for consumer goods company in Germany. Analyze sales data, create demand forecasts, coordinate with production, optimize inventory levels.', 5, 8, 4, '€52,000 - €68,000 EUR', 'Supply Chain/Statistics degree, 4+ years demand planning, Forecasting software experience, German language skills, Analytical thinking', 0, 1, '2025-09-29T11:30:00.0000000', '2025-08-05T03:59:31.2040477', 0, 2),
                    (90, 'Pool Maintenance Technician', 'Maintain swimming pools and water features for luxury resorts in Netherlands. Test water chemistry, clean filters, repair equipment, ensure safety standards, handle chemicals safely.', 6, 10, 5, '€30,000 - €38,000 EUR', 'Pool maintenance certification, 3+ years pool maintenance, Chemical handling license, Dutch language basic, Physical fitness', 0, 1, '2025-09-15T09:00:00.0000000', '2025-08-05T03:59:31.2040479', 0, 2),
                    (91, 'Clinical Psychologist', 'Provide psychological services at mental health clinic in Ireland. Conduct assessments, provide therapy, develop treatment plans, maintain case notes, work with multidisciplinary team.', 2, 12, 6, '€55,000 - €72,000 EUR', 'Psychology PhD, Clinical psychology license, 5+ years clinical experience, English fluency, Therapy specializations', 1, 1, '2025-08-31T12:45:00.0000000', '2025-08-05T03:59:31.2040482', 0, 2),
                    (92, 'Crane Operator', 'Operate tower cranes for high-rise construction projects in Sweden. Safely lift and position materials, coordinate with ground crew, perform daily inspections, maintain safety protocols.', 3, 14, 7, 'SEK 480,000 - 620,000', 'Crane operator license, 6+ years tower crane experience, Safety certifications, Swedish language skills, Height tolerance', 1, 1, '2025-08-26T15:30:00.0000000', '2025-08-05T03:59:31.2040485', 1, 2),
                    (93, 'Pastry Chef', 'Create artisanal pastries and desserts for luxury hotel in Denmark. Design dessert menus, manage pastry team, ensure quality standards, develop new recipes, control costs.', 4, 5, 8, 'DKK 420,000 - 540,000', 'Pastry Arts degree, 6+ years pastry experience, Creative skills, Danish language advantage, Team management', 0, 1, '2025-10-05T13:15:00.0000000', '2025-08-05T03:59:31.2040488', 0, 2),
                    (94, 'Logistics Analyst', 'Analyze logistics operations and performance metrics for shipping company in Norway. Create reports, identify improvement opportunities, optimize routes, reduce transportation costs.', 5, 7, 9, 'NOK 490,000 - 630,000', 'Logistics/Analytics degree, 4+ years logistics analysis, Excel/SQL proficiency, Norwegian language proficiency, Data visualization skills', 0, 1, '2025-09-22T10:45:00.0000000', '2025-08-05T03:59:31.2040490', 0, 2),
                    (95, 'Window Cleaner - High Rise', 'Professional window cleaning for high-rise buildings in Finland. Clean exterior windows, maintain equipment, ensure safety protocols, work at heights using specialized equipment.', 6, 9, 10, '€35,000 - €42,000 EUR', 'High-rise cleaning certification, 4+ years window cleaning, Safety harness training, Finnish language basic, No fear of heights', 0, 1, '2025-09-12T14:00:00.0000000', '2025-08-05T03:59:31.2040493', 0, 2),
                    (96, 'Biomedical Engineer', 'Design and maintain medical equipment for hospital in Poland. Install medical devices, provide technical support, conduct safety inspections, train medical staff on equipment use.', 2, 11, 11, 'PLN 140,000 - 180,000', 'Biomedical Engineering degree, 5+ years medical equipment experience, Polish language skills, Technical troubleshooting, FDA regulations knowledge', 0, 1, '2025-10-03T11:20:00.0000000', '2025-08-05T03:59:31.2040496', 0, 2),
                    (97, 'Fire Safety Engineer', 'Design fire protection systems for commercial buildings in Qatar. Conduct fire risk assessments, design sprinkler systems, ensure code compliance, provide safety training.', 3, 13, 12, '$6,500 - $8,800 USD', 'Fire Protection Engineering degree, 7+ years fire safety design, NFPA standards knowledge, Arabic language advantage, Safety certifications', 1, 1, '2025-08-30T16:45:00.0000000', '2025-08-05T03:59:31.2040498', 0, 1),
                    (98, 'Catering Manager', 'Oversee catering operations for airline catering company in Bahrain. Manage food production, ensure quality standards, coordinate flight schedules, control costs, maintain food safety.', 4, 6, 13, '$4,500 - $6,200 USD', 'Culinary/Hospitality Management degree, 8+ years catering management, HACCP certification, Arabic and English fluency, Airline catering experience', 0, 1, '2025-10-08T12:30:00.0000000', '2025-08-05T03:59:31.2040499', 1, 1);
                SET IDENTITY_INSERT [dbo].[Jobs] OFF;
            ");
        }
    }

    private static string Hash(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}