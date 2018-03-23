var TopDownGame = TopDownGame || {};

//loading the game assets
TopDownGame.Over = function () { };

TopDownGame.Over.prototype = {
    create: function () {
        this.stateText = this.game.add.text(500, 300, ' ', { font: '50px Arial', fill: '#F2F2F2' });
        this.stateText.anchor.setTo(.5, .5);
    },
    update: function () {
        this.stateText.text = "GAME OVER \n Press Space to Restart";
        this.stateText.visible = true;

        this.game.input.onDown.addOnce(function () {
            console.log("game state started");
            TopDownGame.game.state.start('Game')
        });
    },
};