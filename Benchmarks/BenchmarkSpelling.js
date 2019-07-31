const fs = require("fs");
const chalk = require("chalk");
const path = require("path");
const Hunspell = require("./Hunspell");
const SymSpell = require("./SymSpell");
const SpellChecker = require("spellchecker");

const testData = path.resolve("./ABODAT.643");

var lineReader = require("readline").createInterface({
	input: fs.createReadStream(testData)
});

async function _getHunspellSuggestion(word) {
	let spell = await Hunspell.getSpell();

	let suggestions = [];
	if (!spell.correct(word)) {
		suggestions = await spell.suggest(word);
	}

	return suggestions;
}

async function _getSpellCheckerSuggestion(word) {
	let suggestions = [];
	if (SpellChecker.isMisspelled(word)) {
		suggestions = SpellChecker.getCorrectionsForMisspelling(word);
	}

	return suggestions;
}

async function _getSymHunSuggestions(word) {
	try {
		let suggestions = [];
		let symSug = (await SymSpell.lookup(word, 2, 1)).data;

		if (symSug.length !== undefined) {
			symSug = [];
		}
		symSug = symSug.filter(sug => sug.distance !== 0);
		if (symSug.length > 4) {
			symSug = symSug.slice(0, 3);
		}
		symSug = symSug.map(sug => sug.term);

		if (SpellChecker.isMisspelled(word)) {
			suggestions = SpellChecker.getCorrectionsForMisspelling(word);
		}

		return suggestions;
	} catch (err) {
		c++;

		let suggestions = [];

		if (SpellChecker.isMisspelled(word)) {
			suggestions = SpellChecker.getCorrectionsForMisspelling(word);
		}

		return suggestions;
	}
}

const Algorithm = Object.freeze({
	Hunspell: 0,
	SymSpell: 1,
	SpellChecker: 3,
	Combined: 4
});

function Benchmark() {
	this.phrases = [];
	this.evaluate = function(data, phrasesRes) {
		let truePositive = 0;
		let trueNegative = 0;
		let falsePositive = 0;
		let falseNegative = 0;
		data.forEach((suggestions, i) => {
			if (i % 2 === 0) {
				let word = phrasesRes[i].word.toLowerCase();
				suggestions = suggestions.map(s => s.toLowerCase());

				if (suggestions.indexOf(word) > -1) {
					// incorrect word is classified as incorrect and give correct suggestions
					truePositive++;
				} else {
					// incrorrect word is classified as incorrect but gives wrong suggestions
					falseNegative++;
					//console.log(word, phrasesRes[i].incWord, suggestions);
				}
			} else {
				if (suggestions.length !== 0) {
					// correct word is classified as incorrect
					falsePositive++;
				} else {
					// correct word is classified as correct

					trueNegative++;
				}
			}
		});

		return { truePositive, trueNegative, falsePositive, falseNegative };
	};
	this.readLines = async function() {
		return new Promise((resolve, reject) => {
			lineReader
				.on("line", line => {
					if (line.charAt(0) !== "$") {
						let phrases = line.split(", ");
						phrases = phrases.map(s => s.split(" "));
						phrases = phrases.map(s => {
							let incWord = s[0];
							let word = s[1];
							incWord = incWord.replace(",", "");
							word = word.replace(",", "");
							word = word.trim();
							incWord = incWord.replace(".", "");
							incWord = incWord.trim();
							word = word.replace(".", "");
							this.phrases.push([incWord, word]);
						});
					}

					// let [incWord, word] = line.split(",");
					// word = word.replace(",", "");
					// word = word.trim();
					// incWord = incWord.replace(",", "");
					// incWord = incWord.trim();

					// this.phrases.push([incWord, word]);
				})
				.on("close", () => {
					resolve();
				})
				.on("error", err => {
					reject(err);
				});
		});
	};

	this.init = async function() {
		if (this.phrases.length === 0) {
			await this.readLines();
		}
	};
	this.perform = async function(algorithm) {
		if (this.phrases.length === 0) {
			await this.readLines();
		}

		return new Promise((resolve, reject) => {
			let promise = [];
			let phrasesRes = [];
			//this.phrases = this.phrases.slice(0, 1000);

			this.phrases.forEach(s => {
				let word = s[1];
				let incWord = s[0];

				phrasesRes.push({ word, incWord });
				phrasesRes.push({ word, incWord });

				if (algorithm === Algorithm.Hunspell) {
					promise.push(_getHunspellSuggestion(incWord));
					promise.push(_getHunspellSuggestion(word));
				}

				if (algorithm === Algorithm.SymSpell) {
					promise.push(SymSpell.lookup(incWord.toLowerCase(), 2, 1, 1));
					promise.push(SymSpell.lookup(word.toLowerCase(), 2, 1, 1));
				}

				if (algorithm === Algorithm.SpellChecker) {
					promise.push(_getSpellCheckerSuggestion(incWord));
					promise.push(_getSpellCheckerSuggestion(word));
				}

				if (algorithm === Algorithm.Combined) {
					promise.push(_getSymHunSuggestions(incWord));
					promise.push(_getSymHunSuggestions(word));
				}
			});

			Promise.all(promise)
				.then(data => {
					resolve(this.evaluate(data, phrasesRes));
				})
				.catch(err => {
					reject(err);
				});
		});
	};
}
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

const main = async function() {
	let timer = new Timer();
	timer.start();
	let benchmark = new Benchmark();
	await benchmark.init();
	timer.end().print("Lines");

	// timer.start();
	// let result = await benchmark.perform(Algorithm.SymSpell);
	// console.log("SymSpell", result);
	// timer.end().print("SymSpell");

	// timer.start();
	// result = await benchmark.perform(Algorithm.SpellChecker);
	// console.log("SpellChecker", result);
	// timer.end().print("SpellChecker");

	timer.start();
	result = await benchmark.perform(Algorithm.Hunspell);
	console.log("Hunspell", result);
	timer.end().print("Hunspell");
};

main();
