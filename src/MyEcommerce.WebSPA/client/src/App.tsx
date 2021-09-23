import React, { useState, useEffect } from 'react';
import { Configuration, ProductsApi } from 'typescript-axios-product-service';
import logo from './logo.svg';
import './App.css';

function App() {
  const [products, setProducts] = useState<any>({});

  useEffect(() => {
    getProducts();
  }, []);

  const getProducts = async () => {
    const productsApiService = new ProductsApi(new Configuration({ basePath: "https://localhost:6001"}));
    const response = await productsApiService.getAll();
    console.log(response);
    setProducts(response);
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
