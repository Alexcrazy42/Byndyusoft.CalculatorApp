import { useState } from 'react';
import { makeCalculationRequest } from '../services/calculationService';

function App() {
  const [input, setInput] = useState('');
  const [resultText, setResultText] = useState('');

  const Calculate = () => {
    makeCalculationRequest(input)
    .then((result) => {
      setResultText(result);
    });
  };

  return (
    <>
      <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          placeholder="Введите данные"
        />
        <button onClick={Calculate}>Вычислить</button>
        <div>
          <p>{resultText}</p>
        </div>
    </>
  )
}

export default App
