let initialState = {
  languages: {
    russian: {
      registration: 'Регистрация',
      isHaveAcc: 'Уже есть аккаунт?',
      login: 'Войти',
    },
    english: {
      registration: 'Registration',
      isHaveAcc: 'Did you have account?',
      login: 'Login',
    },
  },
  currentLanguage: 'english',
};

let formReducer = (state = initialState, action) => {
  return state;
};

export default formReducer;
