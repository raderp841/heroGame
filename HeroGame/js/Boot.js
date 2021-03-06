var TopDownGame = TopDownGame || {};

TopDownGame.Boot = function(){};

//setting game configuration and loading the assets for the loading screen
TopDownGame.Boot.prototype = {
  preload: function() {
    //assets we'll use in the loading screen
    this.load.image('preloadbar', '../assets/images/preloader-bar.png');
  },
  create: function() {
    //loading screen will have a white background
    this.game.stage.backgroundColor = '#fff';

    //scaling options
    this.game.scale.fullScreenScaleMode = Phaser.ScaleManager.EXACT_FIT;


    
    //have the game centered horizontally
    this.scale.pageAlignHorizontally = true;
    this.scale.pageAlignVertically = true;
    this.scale.refresh();


    //function to set the size of canvas to fit screen
    function adjust() {
        var divgame = document.getElementById("game");
        divgame.style.width = window.innerWidth + "px";
        divgame.style.height = window.innerHeight + "px";
    }
    
        this.game.input.maxPointers = 1;
        this.game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
        this.game.scale.pageAlignHorizontally = true;
        this.game.scale.pageAlignVertically = true;
        this.game.scale.refresh();
        adjust();
     


    //physics system
    this.game.physics.startSystem(Phaser.Physics.ARCADE);

    //level
   
    
    this.state.start('Preload');
  }
};
