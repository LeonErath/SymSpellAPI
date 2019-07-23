# SymSpellAPI
A REST API for the SymSpell algorithm

## Get Started

```
git clone https://github.com/LeonErath/SymSpellAPI
```

## Docker Container

To build and run the container:
```
docker build -t symspellapi .
docker run -it --rm -p 5000:80 symspellapi
```
The container should be then availabe on http://localhost:5000

# API


### `POST` Lookup


**URL**

```sh
http://localhost:5000/lookup
```


**Parameters**

`document: string`

`distance: int`

`verbosity: int (0 Top,1 Closet,2 All)`


**Output**

```json
[
  {
    "term": "development",
    "distance": 2,
    "count": 286291411
  }
]
```


### `POST` LookupCompound


**URL**

```sh
http://localhost:5000/lookupcompound
```


**Parameters**

`document: string`

`distance: int`



**Output**

```json
[
  {
    "term": "where is the love he had dated for much of the past who couldn't read in sixth grade and inspired him",
    "distance": 9,
    "count": 300000
  }
]
```


### `POST` Word Stemming


**URL**

```sh
http://localhost:5000/wordstemming
```


**Parameters**

`document: string`

`distance: int`



**Output**

```json
{
  "item1": "the quick brown fox jumps over the lazy dog",
  "item2": "the quick brown fox jumps over the lazy dog",
  "item3": 8,
  "item4": -34.49116798191063
}
```
