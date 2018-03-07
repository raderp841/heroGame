

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
class			varchar(20) not null,
lvl				int			not null,
health			int			not null,
heroName		varchar(20) not null,
userInfoId		int			not null,

constraint fk_heroes_userInfo foreign key (userInfoId) references userInfo(id),
);

create table inventory
(
id				int			primary key			identity(1,1),
coins			int			not null, 
heroesId		int			not null,

constraint fk_inventory_heroes foreign key (heroesId) references heroes(id),
);

create table weapon
(
id				int			primary key			identity(1,1),
weaponName		varchar(20) not null,
damage			int			not null,
isRanged		bit			not null,
price			int			not null,
);

create table armor
(
id				int			primary key			identity(1,1),
armorName		varchar(20) not null,
rating			int			not null,
price			int			not null,
);

create table npc
(
id				int			primary key			identity(1,1),
npcName			varchar(20) not null,
isOpen			bit			not null,
inventoryId		int			not null,

constraint fk_npc_inventory foreign key (inventoryId) references inventory(id),
);

create table weaponInventory
(
id				int			primary key			identity(1,1),
weaponId		int			not null,
inventoryId		int			not null,

constraint fk_weaponInventory_weapon    foreign key (weaponId) references weapon(id),
constraint fk_weaponInventory_inventory foreign key (inventoryId) references inventory(id),
);

create table armorInventory
(
id				int			primary key			identity(1,1),
armorId			int			not null,
inventoryId		int			not null,

constraint fk_armorInventory_armor		foreign key (armorId) references armor(id),
constraint fk_armorInventory_inventory	foreign key (inventoryId) references inventory(id),
);