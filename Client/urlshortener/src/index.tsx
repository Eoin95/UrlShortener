import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Redirecter from './helpers/Redirecter';

const pathName = window.location.pathname.slice(1);
const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);

if (pathName) {
  Redirecter(pathName);
}

root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
