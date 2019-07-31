let axios = require("axios");
axios = axios.create({
	baseURL: "http://localhost:5000/api",
	timeout: 1000
});

function formatLookup(response) {
	try {
		if (response.data === undefined) console.log(response);
		response = response.data;
		if (response.length > 3) {
			response = response.slice(0, 3);
		}
		if (response.length === 0) {
			return [];
		}
		if (response[0].distance === 0) {
			return [];
		}

		response = response.map(r => r.term);
		return response;
	} catch (error) {
		console.log(error);

		return [];
	}
}

const lookup = async (
	document,
	distance = 2,
	verbosity = 1,
	lastTimeout = 1
) => {
	try {
		lastTimeout || (lastTimeout = 500);
		const response = await axios.post("/lookup/json", {
			document: document,
			distance: distance,
			verbosity: verbosity
		});

		return formatLookup(response);
	} catch (error) {
		let response = await lookup(document, distance, verbosity, lastTimeout * 2);

		return response;
	}
};

// const lookup = async (document, distance = 1, verbosity = 0) => {
// 	return axios
// 		.get(url + "/lookup", {
// 			params: {
// 				document: document,
// 				distance: distance,
// 				verbosity: verbosity
// 			}
// 		})
// 		.then(function(response) {
// 			return response;
// 		})
// 		.catch(function(error) {
// 			throw error;
// 		});
// };
const wordStemming = async (document, distance = 2) => {
	return axios
		.post("/stemming/json", {
			document: document,
			distance: distance
		})
		.then(function(response) {
			return response;
		})
		.catch(function(error) {
			throw error;
		});
};

const lookupCompound = async (document, distance = 1) => {
	return axios
		.post(url + "/compound/json", {
			document: document,
			distance: distance
		})
		.then(function(response) {
			return response;
		})
		.catch(function(error) {
			throw error;
		});
};

module.exports = {
	lookup,
	lookupCompound,
	wordStemming
};
