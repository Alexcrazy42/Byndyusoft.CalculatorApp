import { useState } from 'react';
import { sendDataToBackend } from '../services/calculationService';

function App() {
  const [inputValue, setInputValue] = useState('');
  const [backendData, setBackendData] = useState('');

  const Calculate = () => {
    sendDataToBackend(inputValue)
    .then((result) => {
      setBackendData(result);
    })
    .catch(() => {
      
    });
  };

  return (
    <>
      <input
          type="text"
          value={inputValue}
          onChange={(e) => setInputValue(e.target.value)}
          placeholder="Введите данные"
        />
        <button onClick={Calculate}>Вычислить</button>
        <div>
          <p>{backendData}</p>
        </div>
    </>
  )
}

export default App
