import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import store from './redux/redux';
import { BrowserRouter } from 'react-router-dom';

ReactDOM.render(
  <BrowserRouter>
    <App store={store} />
  </BrowserRouter>,
  document.getElementById('root')
);

reportWebVitals();
