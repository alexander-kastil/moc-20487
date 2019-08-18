$postParams = "{'origin': 'Germany',
    'destination': 'France',
    'flightNumber': 'GF7625',
    'departureTime': '0001-01-01T00:00:00'}"
Invoke-WebRequest -Uri http://apWebSQL.azurewebsites.net/api/flights -ContentType "application/json" -Method POST -Body $postParams