# 20-487

## Readings

[Fetch Api](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API)

[Markdown Shortcuts](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet)

Sample:

```javascript
fetch('http://example.com/movies.json')
  .then(function(response) {
    return response.json();
  })
  .then(function(myJson) {
    console.log(JSON.stringify(myJson));
  });
```