import{
  incorrectEmail, 
  minLength, 
  shouldContain
} from "../../components/Notifications/Messages"

export const checkEmail = (role: object, value: string, callback:any) => {
    const reg = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (value.length === 0) {
      return callback('');
    }
    if (reg.test(value) === false) {
        return callback(incorrectEmail);
    }
      return callback();
  };

  export const checkNameSurName = (role: object, value: string, callback:any) => {
    const reg = /^[a-zA-Zа-яА-ЯІіЄєЇїҐґ']{1,25}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ']{1,25})*$/;
      if (value.length !== 0 && reg.test(value) === false) {
        return callback(shouldContain('only letters and be shorter that 25 characters'));
      }
      return callback();
  };
  
  
  export const checkPassword = (role: object, value: string, callback:any) => {
    const reg = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/;
    if(value.length > 0)
    {
      if (value.length < 8)
      {
        return callback(minLength(8));
      }
      if (reg.test(value) === false) 
      {
        return callback(shouldContain('characters(at least 1 uppercase), digits та signs'));
      }
    }
      return callback();
  };

  export const checkStartEnd = (role: object, value: string, callback:any) => {
    const reg = /^-?\d+\.?\d*$/;
      if (reg.test(value) === false) 
      {
        return callback(shouldContain('only digits'));
      }
      return callback();
  };
  
  export const checkFunc = (role: object, value: string, callback:any) => {
    const reg = /^-?\d+\.?\d*$/;
      if (reg.test(value) === false) 
      {
        return callback(shouldContain('only digits'));
      }
      return callback();
  };