class Auth {
  getToken() {
    const token = localStorage.getItem("token");
    if (token) {
      return token;
    }
    return null;
  }
}

export default Auth;
