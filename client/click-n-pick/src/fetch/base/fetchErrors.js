class FetchError extends Error {
  constructor(message, errors) {
    super(message);
    this.message = message;
    this.errors = errors;
  }
}

export default FetchError;
