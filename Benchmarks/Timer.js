const chalk = require("chalk");

function Timer() {
	this.startTime = 0;
	this.endTime = 0;
	this.start = () => {
		this.startTime = +new Date();
	};
	this.end = () => {
		this.endTime = +new Date();
		return this;
	};
	this.print = algorithm => {
		console.log(
			chalk.green(
				algorithm,
				"finnished in",
				chalk.bold((this.endTime - this.startTime) / 1000),
				"sec."
			)
		);
	};
}
module.exports = Timer;
