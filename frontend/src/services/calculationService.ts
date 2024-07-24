import axios from 'axios';

export const sendDataToBackend = async (input: string) => {
    try {
      const url = 'https://localhost:7086/api/v1/calculations';
      const headers = {
        'Content-Type': 'application/json',
        'accept': 'text/plain'
      };
      const data = {
        expression: input
      };
  
      const response = await axios.post(url, data, { headers });
  
      if (response.status === 200) {
        const { isError, isExceptedError, data: { result } } = response.data;
        if (!isError && !isExceptedError) {
          return result;
        } else {
          return response.data.message;
        }
      } else {
        return "Ошибка!";
      }
    } catch (error) {
      console.error('Произошла ошибка при запросе:', error.message);
    }
};  