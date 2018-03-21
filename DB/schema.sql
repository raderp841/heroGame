

--Creating Tables

create table userInfo
(
	id				int			primary key			identity(1,1),
	firstName		varchar(20) not null,
	lastName		varchar(20) not null,
	email			varchar(50) not null,
	password		varchar(20) not null,
	passwordSalt	varchar(max)null,
	isAdmin			bit			not null	
);



create table heroes
(
	id				int			primary key			identity(1,1),
	heroName		varchar(20) not null,
	userInfoId		int			not null,
	class			varchar(20) not null,	
	health			int			not null			default 3,
	coins			int			not null			default 0, 	
	
	constraint fk_heroes_userInfo foreign key (userInfoId) references userInfo(id),
);



create table weapon
(
	id				int			primary key			identity(1,1),
	weaponName		varchar(20) not null,	
	damage			int			not null,	
	price			int			not null,
);

create table armor
(
	id				int			primary key			identity(1,1),
	armorName		varchar(20) not null,	
	rating			int			not null,
	price			int			not null,
);


create table weaponInventory
(
	id				int			primary key			identity(1,1),
	weaponId		int			not null,
	heroesId		int			not null,

	constraint fk_weaponInventory_weapon    foreign key (weaponId) references weapon(id),
	constraint fk_weaponInventory_heroes foreign key (heroesId) references heroes(id),
);

create table armorInventory
(
	id				int			primary key			identity(1,1),
	armorId			int			not null,
	heroesId		int			not null,

	constraint fk_armorInventory_armor		foreign key (armorId) references armor(id),
	constraint fk_armorInventory_heroes		foreign key (heroesId) references heroes(id),
);