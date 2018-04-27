/*
Navicat SQL Server Data Transfer

Source Server         : ProduccionSQLServer
Source Server Version : 140000
Source Host           : 188.165.208.26\PRODUCCION,1433:1433
Source Database       : SanCristobal
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 140000
File Encoding         : 65001

Date: 2018-04-27 01:16:43
*/


-- ----------------------------
-- Table structure for user_locations
-- ----------------------------
DROP TABLE [dbo].[user_locations]
GO
CREATE TABLE [dbo].[user_locations] (
[user_id] int NOT NULL ,
[location] geography NULL ,
[timestamp] bigint NULL 
)


GO

-- ----------------------------
-- Records of user_locations
-- ----------------------------
INSERT INTO [dbo].[user_locations] ([user_id], [location], [timestamp]) VALUES (N'13', geography::STGeomFromText('POINT (-58,419961 -34,5876872)',4326), N'1524797835')
GO
GO
INSERT INTO [dbo].[user_locations] ([user_id], [location], [timestamp]) VALUES (N'14', geography::STGeomFromText('POINT (-58,3656437 -34,6153572)',4326), N'1524792120')
GO
GO
INSERT INTO [dbo].[user_locations] ([user_id], [location], [timestamp]) VALUES (N'15', geography::STGeomFromText('POINT (-58,5046640544685 -34,4780526296152)',4326), N'1524541447')
GO
GO
INSERT INTO [dbo].[user_locations] ([user_id], [location], [timestamp]) VALUES (N'16', geography::STGeomFromText('POINT (-58,4008268 -34,630271)',4326), N'1524578977')
GO
GO

-- ----------------------------
-- Table structure for user_services
-- ----------------------------
DROP TABLE [dbo].[user_services]
GO
CREATE TABLE [dbo].[user_services] (
[service_id] int NOT NULL IDENTITY(1,1) ,
[client_user_id] int NULL ,
[crane_user_id] int NULL ,
[location] geography NULL ,
[request_timestamp] bigint NULL ,
[taken_timestamp] bigint NULL DEFAULT ((0)) ,
[completed_timestamp] bigint NULL DEFAULT ((0)) ,
[description] varchar(255) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[user_services]', RESEED, 25)
GO

-- ----------------------------
-- Records of user_services
-- ----------------------------
SET IDENTITY_INSERT [dbo].[user_services] ON
GO
INSERT INTO [dbo].[user_services] ([service_id], [client_user_id], [crane_user_id], [location], [request_timestamp], [taken_timestamp], [completed_timestamp], [description]) VALUES (N'22', N'14', N'13', geography::STGeomFromText('POINT (-58,5076911 -34,5614423)',4326), N'1524778363', N'1524778661', N'1524778667', null)
GO
GO
INSERT INTO [dbo].[user_services] ([service_id], [client_user_id], [crane_user_id], [location], [request_timestamp], [taken_timestamp], [completed_timestamp], [description]) VALUES (N'23', N'14', N'13', geography::STGeomFromText('POINT (-58,5050652176142 -34,559401780355)',4326), N'1524779000', N'1524779154', N'1524779760', N'El auto no arranca')
GO
GO
INSERT INTO [dbo].[user_services] ([service_id], [client_user_id], [crane_user_id], [location], [request_timestamp], [taken_timestamp], [completed_timestamp], [description]) VALUES (N'24', N'14', N'13', geography::STGeomFromText('POINT (-58,5064381733537 -34,5604258764461)',4326), N'1524779891', N'1524780087', N'1524780253', N'no enciende')
GO
GO
INSERT INTO [dbo].[user_services] ([service_id], [client_user_id], [crane_user_id], [location], [request_timestamp], [taken_timestamp], [completed_timestamp], [description]) VALUES (N'25', N'14', N'13', geography::STGeomFromText('POINT (-58,5059785097837 -34,5604137276313)',4326), N'1524781182', N'1524781191', N'0', N'no enciende')
GO
GO
SET IDENTITY_INSERT [dbo].[user_services] OFF
GO

-- ----------------------------
-- Table structure for user_tokens
-- ----------------------------
DROP TABLE [dbo].[user_tokens]
GO
CREATE TABLE [dbo].[user_tokens] (
[token] varchar(255) NOT NULL ,
[expire_timestamp] bigint NULL ,
[user_id] int NOT NULL 
)


GO

-- ----------------------------
-- Records of user_tokens
-- ----------------------------
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'4FWyDVTGvNT4Hcc7XPoehZamatvBWZYYuLtfCzRwAe3ygdhbjKHGWH7sxBNo', N'1540333147', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'4GLuh7Czi8PPCmvz9iwmWjndiFqR7s2ToEdnCwhkA2nwuTiZpcDeqCmnGLun', N'1540331744', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'6dVhjnAi7yCui8YXpizHVqCEeeMFRp7svcf3ZTSNw3KAFoK6Rd3T3vgepZgL', N'1540300421', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'7qerzHoJHbJaHDJwBnto4tXyPTeFvGSbwgkh7g2w8by4sFXuz9orLmoA3Qmu', N'1540331631', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'7vdxidjRvJLe5qTMEFLGJLNE8Kygu7XzvFqvY8RJ7uf3MhswCe2oMpd3qRtP', N'1540153905', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'8vz367xR84wjK88KBwyYv5MxJ7aMyiEtCFttfM7jojca6uekvpsGAwBa6fwr', N'1540151288', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'aVszDCHz4BNh6oG7pMCxBrmbSMkvyytL2Q3fmevrt7jDiCVzdubBFVqbAYGo', N'1540158329', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'ayNhNznSYQSmdGLt3vWK8AdcVcVJmWckY2UmqhXicmtxynbwv4BQ98ySBwuQ', N'1540331637', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'BbgjPZEN9cVQeZSoLfHekfNBcbA2wDCZ4ZuaneWFVwVdQhfEihekjmyuAfro', N'1540332789', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'bGmjgma6354sshvETNz3zRwJ7nffbrB8FhDBvPKyus8QtLnpoSyAGQCXBdni', N'1540170643', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'BhJdBnmBHWykexwatppdNQbomjEacgmmxYizbVjzVyDpoEVkVvuh7fYAiSZV', N'1540093270', N'15')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'bJmnb9yYKfhaMbQe4sfomB7hirbFHVYkBtx24sVhsygZhh9poA4PPdidZYU7', N'1540331636', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'bqFzpWh3okhxc2nzniha47rdiqLFKu5mJubQCqXYjdMypScxi6QPVVHrFyhV', N'1540332053', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'chaBbPCFvDaAhHsrAxPnLmdjYusacbruRhaYtunezcYm7EVTu6C8AwJWQRrb', N'1540162834', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'DoiqU2YTpJo6KPXeAGmqVspbjnwFhwcRjLKh3FWPJ6jsuEeVPati2Y5ikRGV', N'1540300852', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'EjGvJjetuFuDJxXTCWDxhEqKjAeWpM8VE2shaHtv7fTDw4mPeYgB5NrNsTaG', N'1540332596', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'es2Ui8tpGEmgTfvrHX5KqoPLfhJapsCf9Wddpms2322BQhnnBPhuAdTNzHMp', N'1540088454', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'hbfW9Xc7jjHyc5mBFa8C72wAoG7nJomRnVhN34pLGMUreihhheyJX9utJmnc', N'1540332785', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'Htte9J2WoqJtg9LVBJY5TKf5f7FPFwqzNwfPnMxg7vpy4i6DBGMGEWritZJX', N'1540159538', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'ki2QC3byv7bmnE7a6FnsTpZCWP4Xn7jsy6xpiKFYTbVpjn7LjAa4Mze4ZrCH', N'1540159292', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'kWfKFXgr95ncU99xRx9fmkJsipSGdtMVDYzui43qWtBt7ZBArJv3JmNcykNj', N'1540088690', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'LyFN9GpRzdUJ3TCjnv9byUeTvmnUWACuPSh8gv9osjr5JCZGmCxkFUJzTqez', N'1540328996', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'n8vQnqveWeMmGFfK3Sx8Hfp4YZgHarq6eRHzCRTfrNcA3U6ooNFUZRw2TFad', N'1540159156', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'ofdmTcWbHYoHfHNfusVZ54RkpwycW32cizvSwrYrNHjzFBGGBMjNtBFi4cuX', N'1540296919', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'oMjesgiDECkLqEczvCGPfc7ys7agMkGcdMZiCkhznF73FuPnnfmnt9UjBxV6', N'1540093231', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'oxABVmJtnLLz7yCNW7cB7voagw5vJtBdGgRchkkG32HikJAUvAszYzEbRCGN', N'1540158482', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'oxPswj2UbP6DxSFFdbqTcVrwQZCQZxs9bH2GadFCR56f6tbHp3W2XtsCYzA7', N'1540159685', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'PHimWwoEWQAFQUfLLswLrBzY3YjeTWCz8xdSC4FhRUcriXpo3LZvDvXNSShH', N'1540092884', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'qMRg2am8RMCJdaghU7VnETs2nCcS2ZD5w4ofNYWfMBbwtmj4ZriAMPTjP4LW', N'1540093257', N'15')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'Qoc2a2FMYEaP9bmww3fzSj5RJjhUrjzuArYJ26PuJDoVmUy5vzeHHLUQqjgx', N'1540154268', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'r2RQNK8jEznGMbBmo6PxEFrPMnszNYKxCxkiByGgHegkyNdeagh4RBZGxrpF', N'1540130579', N'16')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'RoheEtdGseZTWpWRVMhWvsPsrxB8Xjoj37GYQCFYJ3RCapwtQXwgnaYDmGRV', N'1540093215', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'ScNmuQEpZ6euCNxxKMe4dETwbBv6nj22iMC2n4CiicjEMQUcHaycPYSZfzTb', N'1540130594', N'16')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'T9opVWqBbNVwfAqyDTPf9YPwGTBotskjeuHufJei3MzWwqcttAR2ksXveCpn', N'1540329274', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'td3VXCaFMJDs2iBaeZnvBLwq3AK6zuJ5p8isP7dtP6K3q9AF5Tx6CLNf6YxA', N'1540332788', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'tD72voredog3nGiqi9GVSntdoXVCKfSn3JJiT8M9sQb59CdQ5nPaSVrh5pLZ', N'1540158182', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'u2URzozS3AgR2rBKC5diAzdnGJCTayVpUUao5YwcRpgB7FeCbubBGo9udz74', N'1540221968', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'WcR7W6C7xgHGtMggsu54gQ6ArS6GS4h9BKnc3bsTtAiSZ8tbWJavXu8rX2Ri', N'1540329265', N'13')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'wh6AZ2Jgnv744EQJVcsDSzsZ3tk6hyyy4CcYmpoNSkvDerrJzR3o9XybwZVG', N'1540130789', N'16')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'WnQ7RLhgmyUCbkBF5KMRCmRCeritgHuj4KShF6ytM4PiwBBpLsmCa6FuYfAg', N'1540332790', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'xLgrGuPxEc8RtfQUSwtZKduDKHqLyg7ppdpsXQdMtvt4ZhRbDhAoxmTGy5HL', N'1540332737', N'14')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'y7X7fKDKfuaTxXQzreNSQvWj7piTZgwqa4zuCCWeEEW5RCijnmWEkCBUuJgZ', N'1540093315', N'15')
GO
GO
INSERT INTO [dbo].[user_tokens] ([token], [expire_timestamp], [user_id]) VALUES (N'yCXEnUb6tE3gXbVSCvLzF8Joew6kqwEW3EZwvpNSQerFCbpCMytVfcQXPguo', N'1540088463', N'13')
GO
GO

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE [dbo].[users]
GO
CREATE TABLE [dbo].[users] (
[user_id] int NOT NULL IDENTITY(1,1) ,
[mail] varchar(150) NULL ,
[password] varchar(300) NULL ,
[salt] varchar(150) NULL ,
[first_name] varchar(255) NULL ,
[last_name] varchar(255) NULL ,
[creation_timestamp] bigint NULL ,
[mobile_phone] varchar(255) NULL ,
[username] varchar(255) NULL ,
[plate] varchar(7) NULL ,
[is_provider] int NULL ,
[status] varchar(255) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[users]', RESEED, 16)
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'users', 
'COLUMN', N'creation_timestamp')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'Unix timestamp'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'users'
, @level2type = 'COLUMN', @level2name = N'creation_timestamp'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Unix timestamp'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'users'
, @level2type = 'COLUMN', @level2name = N'creation_timestamp'
GO

-- ----------------------------
-- Records of users
-- ----------------------------
SET IDENTITY_INSERT [dbo].[users] ON
GO
INSERT INTO [dbo].[users] ([user_id], [mail], [password], [salt], [first_name], [last_name], [creation_timestamp], [mobile_phone], [username], [plate], [is_provider], [status]) VALUES (N'13', N'mfacundo94@gmail.com', N'ac55559111d0277cb16089e933567a4ea8ab143ab50ead0b4a93a8259e5b17e1', N'e43UoLug', N'Mariano', N'Scigliano ', N'1524536453', N'11-6801-0565', N'mfacundo94@gmail.com', N'AC396OB', N'1', N'unavailable_crane')
GO
GO
INSERT INTO [dbo].[users] ([user_id], [mail], [password], [salt], [first_name], [last_name], [creation_timestamp], [mobile_phone], [username], [plate], [is_provider], [status]) VALUES (N'14', N'rothfrancisco@resto.vip', N'f7a0a20c8db5d2df87e33fe64bb70029772e44a02b472552f85fc4290d673f70', N'YneLUPye', N'Francisco', N'Roth', N'1524541215', N'11-5721-2809', N'FRoth', N'FZO573', N'0', N'waiting_crane_arrival')
GO
GO
INSERT INTO [dbo].[users] ([user_id], [mail], [password], [salt], [first_name], [last_name], [creation_timestamp], [mobile_phone], [username], [plate], [is_provider], [status]) VALUES (N'15', N'tpulenta@gmail.com', N'ab28666172848f23f96ff6f01510d57b6e8b14bcbf177385b376917ef17b81a9', N'k5VKtbm0', N'Tomas', N'Pulenta', N'1524541257', N'1144373426', N'tomaspulenta', null, N'1', null)
GO
GO
INSERT INTO [dbo].[users] ([user_id], [mail], [password], [salt], [first_name], [last_name], [creation_timestamp], [mobile_phone], [username], [plate], [is_provider], [status]) VALUES (N'16', N'igniparra@gmail.com', N'0887be869d39e8e2208f41c10cd46c63793406b03cf4f8ef45ce8efc0070d119', N'Kn7LUjE9', N'Ignacio', N'Parravicini', N'1524578579', N'1130301595', N'igniparra', null, N'1', null)
GO
GO
SET IDENTITY_INSERT [dbo].[users] OFF
GO

-- ----------------------------
-- Indexes structure for table user_locations
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table user_locations
-- ----------------------------
ALTER TABLE [dbo].[user_locations] ADD PRIMARY KEY ([user_id])
GO

-- ----------------------------
-- Indexes structure for table user_services
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table user_services
-- ----------------------------
ALTER TABLE [dbo].[user_services] ADD PRIMARY KEY ([service_id])
GO

-- ----------------------------
-- Indexes structure for table user_tokens
-- ----------------------------
CREATE UNIQUE INDEX [token] ON [dbo].[user_tokens]
([token] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Primary Key structure for table user_tokens
-- ----------------------------
ALTER TABLE [dbo].[user_tokens] ADD PRIMARY KEY ([token])
GO

-- ----------------------------
-- Indexes structure for table users
-- ----------------------------
CREATE UNIQUE INDEX [user_id] ON [dbo].[users]
([user_id] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [mail for login] ON [dbo].[users]
([mail] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table users
-- ----------------------------
ALTER TABLE [dbo].[users] ADD PRIMARY KEY ([user_id])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[user_locations]
-- ----------------------------
ALTER TABLE [dbo].[user_locations] ADD FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([user_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[user_services]
-- ----------------------------
ALTER TABLE [dbo].[user_services] ADD FOREIGN KEY ([client_user_id]) REFERENCES [dbo].[users] ([user_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[user_services] ADD FOREIGN KEY ([crane_user_id]) REFERENCES [dbo].[users] ([user_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[user_tokens]
-- ----------------------------
ALTER TABLE [dbo].[user_tokens] ADD FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([user_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
