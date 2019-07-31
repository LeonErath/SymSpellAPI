const fs = require("fs");
const path = require("path");
var nspell = require("nspell");

const dictPath = path.resolve("./data/hunspell");

const readFile = fs.readFileSync;

let spell = null;
const initializeSpell = async () => {
	const affBuffer = readFile(path.join(dictPath, "en-GB.aff"));
	const dicBuffer = readFile(path.join(dictPath, "en-GB.dict"));

	spell = nspell(affBuffer, dicBuffer);

	const dic1 = readFile(path.join(dictPath, "spelling_en-GB.txt"));
	const dic2 = readFile(path.join(dictPath, "spelling.txt"));
	const dic3 = readFile(path.join(dictPath, "zusatz.txt"));

	spell.personal(dic1);
	spell.personal(dic2);
	spell.personal(dic3);
};

const getSpell = async () => {
	if (spell === null) await initializeSpell();
	return spell;
};

module.exports = { getSpell };
