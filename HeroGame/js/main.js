var TopDownGame = TopDownGame || {};

TopDownGame.game = new Phaser.Game(1000, 800 ,Phaser.AUTO, 'game');

TopDownGame.game.state.add('Boot', TopDownGame.Boot);
TopDownGame.game.state.add('Preload', TopDownGame.Preload);
TopDownGame.game.state.add('Game', TopDownGame.Game);

TopDownGame.game.state.start('Boot');