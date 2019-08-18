window.addEventListener("load", function() {
  let url = "https://localhost:8080/api/vouchers";
  fetch(url)
    .then(resp => {
      console.log("Response received from fetch", resp);
      return resp.json();
    })
    .then(data => {
      console.log("Data received from fetch", data);
      document.querySelector("#result").innerHTML = JSON.stringify(data);
    });
});
