var TopDownGame = TopDownGame || {};

//title screen
TopDownGame.Game = function () { };

TopDownGame.Game.prototype = {
    create: function () {
        this.health = 100;

        if (this.map == null) {
            this.map = this.game.add.tilemap('level3');
        }

        //the first parameter is the tileset name as specified in Tiled, the second is the key to the asset
        this.map.addTilesetImage('tiles', 'gameTiles');

        //create layer
        this.backgroundlayer = this.map.createLayer('backgroundLayer');
        this.blockedLayer = this.map.createLayer('blockedLayer');

        //collision on blockedLayer
        this.map.setCollisionBetween(1, 2000, true, 'blockedLayer');

        //resizes the game world to match the layer dimensions
        this.backgroundlayer.resizeWorld();

        this.createItems();
        this.createDoors();
        this.createEnemies();

        //create player
        var result = this.findObjectsByType('playerStart', this.map, 'objectsLayer')
        this.player = this.game.add.sprite(result[0].x, result[0].y, 'player');
        this.player.frame = 0;
        this.player.animations.add('right', [16, 17, 18], 10, false);
        this.player.animations.add('up', [32, 33, 34], 10, false);
        this.player.animations.add('down', [0, 1, 2], 10, false);
        this.player.anchor.setTo(.5, .5);
        this.game.physics.arcade.enable(this.player);
        this.isFlipped = false;

        //the camera will follow the player in the world

        //move player with cursor keys
        this.cursors = this.game.input.keyboard.createCursorKeys();

    },
    createItems: function () {
        //create items
        this.items = this.game.add.group();
        this.items.enableBody = true;
        var item;
        result = this.findObjectsByType('item', this.map, 'objectsLayer');
        result.forEach(function (element) {
            this.createFromTiledObject(element, this.items);
        }, this);
    },
    createEnemies: function () {
        this.enemies = this.game.add.group();
        this.enemies.enableBody = true;
        var enemy;
        result = this.findObjectsByType('enemy', this.map, 'objectsLayer');
        result.forEach(function (element) {
            this.createFromTiledObject(element, this.enemies); 
        }, this);
    },
    createDoors: function () {
        //create doors
        this.doors = this.game.add.group();
        this.doors.enableBody = true;
        result = this.findObjectsByType('door', this.map, 'objectsLayer');

        result.forEach(function (element) {
            this.createFromTiledObject(element, this.doors);
        }, this);
    },

    //find objects in a Tiled layer that containt a property called "type" equal to a certain value
    findObjectsByType: function (type, map, layer) {
        var result = new Array();
        map.objects[layer].forEach(function (element) {
            if (element.properties.type === type) {
                //Phaser uses top left, Tiled bottom left so we have to adjust
                //also keep in mind that the cup images are a bit smaller than the tile which is 16x16
                //so they might not be placed in the exact position as in Tiled
                element.y -= map.tileHeight;
                result.push(element);
            }
        });
        return result;
    },
    //create a sprite from an object
    createFromTiledObject: function (element, group) {
        var sprite = group.create(element.x, element.y, element.properties.sprite);

        //copy all properties to the sprite
        Object.keys(element.properties).forEach(function (key) {
            sprite[key] = element.properties[key];
            
        });
    },
    update: function () {
        //collision
        this.game.physics.arcade.collide(this.player, this.blockedLayer);
        this.game.physics.arcade.overlap(this.player, this.items, this.collect, null, this);
        this.game.physics.arcade.overlap(this.player, this.doors, this.enterDoor, null, this);
        this.game.physics.arcade.overlap(this.player, this.enemies, this.fight, null, this);

        //player movement

        this.player.body.velocity.x = 0;

        if (this.cursors.up.isDown) {
            if (this.player.body.velocity.y == 0)
                this.player.body.velocity.y -= 50;
            this.player.animations.play('up');
        }
        else if (this.cursors.down.isDown) {
            if (this.player.body.velocity.y == 0)
                this.player.body.velocity.y += 50;
            this.player.animations.play('down');
        }
        else {
            this.player.body.velocity.y = 0;
        }
        if (this.cursors.left.isDown) {
            if (this.isFlipped) {
                this.player.body.velocity.x -= 50;
                this.player.animations.play('right');
            }
            else {
                this.player.scale.x *= -1;
                this.player.body.velocity.x -= 50;
                this.player.animations.play('right');
                this.isFlipped = true;
            }
        }
        else if (this.cursors.right.isDown) {
            if (this.isFlipped) {
                this.player.scale.x *= -1;
                this.player.body.velocity.x += 50;
                this.player.animations.play('right');
                this.isFlipped = false;
            }
            else {
                this.player.body.velocity.x += 50;
                this.player.animations.play('right');
            }
        }
    },
    collect: function (player, collectable) {
        console.log('yummy!');

        //remove sprite
        collectable.destroy();
    },
    enterDoor: function (player, door) {
        console.log('entering door that will take you to ' + door.targetTilemap + ' on x:' + door.targetX + ' and y:' + door.targetY);
        this.map = this.game.add.tilemap(door.targetTilemap);
        this.state.restart();
    },
    die: function (player, enemy) {
        console.log('youre dead');
        this.player.kill();
        this.game = null;
        this.items = null;
        this.enemies = null;
        this.doors = null;
        healthh = null;
        this.state.start('Over');
    },
    fight: function (player, enemy) {
        function sleep(milliseconds) {
            var start = new Date().getTime();
            for (var i = 0; i < 1e7; i++) {
                if ((new Date().getTime() - start) > milliseconds) {
                    break;
                }
            }
        }
        if (this.health === 0) this.die();
        else {
            this.health -= 1;
            var healthh = this.health;
            sleep(100);
            console.log(healthh);
        }
    },

};