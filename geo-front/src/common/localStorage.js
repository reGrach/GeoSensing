const LOGIN = 'login';
const ROLE = 'role';
const DATA_EXPIRED = 'expirationDate';

export const getAuth = () => {
  const date = new Date(window.localStorage.getItem(DATA_EXPIRED));
  if (date > Date.now()) {
    return {
      isAuth: true,
      login: window.localStorage.getItem(LOGIN),
      role: window.localStorage.getItem(ROLE),
    };
  }
  window.localStorage.removeItem(LOGIN);
  window.localStorage.removeItem(ROLE);
  window.localStorage.removeItem(DATA_EXPIRED);
  return { isAuth: false };
};

export const purgeAuth = () => {
  window.localStorage.removeItem(LOGIN);
  window.localStorage.removeItem(ROLE);
  window.localStorage.removeItem(DATA_EXPIRED);
};

export const setAuth = (login, role, date) => {
  window.localStorage.setItem(LOGIN, login);
  window.localStorage.setItem(ROLE, role);
  window.localStorage.setItem(DATA_EXPIRED, date);
};
