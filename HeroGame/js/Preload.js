var TopDownGame = TopDownGame || {};

//loading the game assets
TopDownGame.Preload = function(){};

TopDownGame.Preload.prototype = {
  preload: function() {
    //show loading screen
    this.preloadBar = this.add.sprite(this.game.world.centerX, this.game.world.centerY, 'preloadbar');
    this.preloadBar.anchor.setTo(0.5);

    this.load.setPreloadSprite(this.preloadBar);

    //load game assets
    this.load.tilemap('level1', '../assets/tilemaps/level1.json', null, Phaser.Tilemap.TILED_JSON);
    this.load.tilemap('level2', '../assets/tilemaps/level2.json', null, Phaser.Tilemap.TILED_JSON);
    this.load.tilemap('level3', '../assets/tilemaps/level3.json', null, Phaser.Tilemap.TILED_JSON);
    this.load.spritesheet('player', '../assets/images/playerSprite.png', 16, 16);
    this.load.spritesheet('flame', '../assets/images/flame.png', 16, 16);
    this.load.image('gameTiles', '../assets/images/tiles.png');
    this.load.image('greencup', '../assets/images/greencup.png');
    this.load.image('bluecup', '../assets/images/bluecup.png');
    //this.load.image('player', '../assets/images/player.png');
    this.load.image('browndoor', '../assets/images/browndoor.png');
    
  },
  create: function() {
    this.state.start('Game');
  }
};