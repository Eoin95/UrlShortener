import { useState } from 'react';
import '../App.css';
import { Link } from 'react-router-dom';


export function NotFoundPage() {
  const [loading, setLoading] = useState(true);

  setTimeout(() => setLoading(false), 3000);


  return (

    <div className="App">
      <header className="App-header">
        <p className='text-5xl font-medium mb-8'>
          UrlShortener
        </p>
        {
          loading ?
            <span>Checking url...</span>
            :
            <div>
              <span className='block'>The requested url was not found.</span>
              <button type='button'
                className='text-xl font-medium bg-purple-600 text-white border border-purple-600 rounded hover:bg-purple-800 mt-4 px-4 py-1'>
                <Link to="/">Create a short url</Link>
              </button>
            </div>
        }

      </header>
    </div>
  );
}