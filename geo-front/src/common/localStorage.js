const LOGIN = 'login'
const DATA_EXPIRED = 'expirationDate'

export const getLogin = () => {
  const date = new Date(window.localStorage.getItem(DATA_EXPIRED))
  if (date > Date.now()) {
    return window.localStorage.getItem(LOGIN)
  } else {
    window.localStorage.removeItem(LOGIN)
    window.localStorage.removeItem(DATA_EXPIRED)
    return null
  }
}

export const removeLogin = (login, date) => {
  window.localStorage.removeItem(LOGIN)
  window.localStorage.removeItem(DATA_EXPIRED)
}

export const saveLogin = (login, date) => {
  window.localStorage.setItem(LOGIN, login)
  window.localStorage.setItem(DATA_EXPIRED, date)
}
