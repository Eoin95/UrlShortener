import '../App.css';
import { Shortenerbox } from './ShortenerBox';

function HomePage() {
  return (
    <div className="App">
      <header className="App-header">
        <p className='text-5xl font-medium mb-8'>
          UrlShortener
        </p>
        <Shortenerbox />
      </header>
    </div>
  );
}

export default HomePage;
