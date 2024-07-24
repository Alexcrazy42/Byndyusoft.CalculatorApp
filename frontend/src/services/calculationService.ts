import axios from 'axios';
import base_url from '../const/url';


export const makeCalculationRequest = async (input: string) => {
    const headers = {
      'Content-Type': 'application/json',
      'accept': 'text/plain'
    };
    const data = {
      expression: input
    };

    try {
      const response = await axios.post(base_url + "/api/v1/calculations", data, { headers });
      const { isError, isExceptedError, message, defaultMessage } = response.data;
      if(!isError){
        const { data: { result } } = response.data;
        return result;
      } else {
        if(isExceptedError) {
          return message;
        } else {
          return defaultMessage;
        }
      }
    } catch (error) {
      if (error.response) {
        return "Ошибка!";

      } else if (error.request) {
        return "Проблема с сетью!"
      } else {
        return "Ошибка!"
      }
    }
};    