<div id="top"></div>

<!-- ABOUT THE PROJECT -->
## About The Project
This repository holds the API backend for Givt V2

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- GETTING STARTED -->
### Prerequisites

_TODO for developing locally_

* npm
  ```sh
  npm install npm@latest -g
  ```

<!-- USAGE EXAMPLES -->
## Usage with docker run

1. Build the backend project
   ```sh
   docker build -t backend .
   ```
2. Run the backend
   ```sh
   docker run -e CUSTOMCONNSTR_AzureAppConfiguration="__CONNECTION_STRING__" -p 5000:5000 backend
   ```
3. Go to localhost:5000/health, which should return HTTP/200
4. Go to localhost:5000/swagger to get an overview of all the API calls available

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Proposal: we can make use of the [github branching flow](https://www.flagship.io/git-branching-strategies/#github-flow)

1. Check out the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>

