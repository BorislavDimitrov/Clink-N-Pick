import Status from "./status";
import OptionsGenerator from "./optionsGenerator";
import ServerResponse from "./serverResponse";
import FetchError from "./fetchErrors";

class Request {
  #statusTypes;
  #optionsGenerator;

  constructor() {
    this.#statusTypes = new Status();
    this.#optionsGenerator = new OptionsGenerator();
  }

  async send(sendDto) {
    try {
      this.#validate(sendDto.method, sendDto.path);
      const options = this.#optionsGenerator.generate(
        sendDto.method,
        sendDto.body,
        sendDto.isFormData,
        sendDto.isSendDataForm
      );
      const response = await this.#performFetch(sendDto.path, options);

      return response;
    } catch (err) {
      if (err.message === this.#statusTypes.badRequest()) {
        return new ServerResponse(this.#statusTypes.badRequest(), err.errors);
      }
      if (err.message === this.#statusTypes.unauthorized()) {
        this.#logoutIfUnauthorized();
      }

      return new ServerResponse(this.#statusTypes.badRequest(), [
        "Please check your internet connection and try again!",
      ]);
    }
  }

  async #performFetch(path, options) {
    const response = await fetch(path, options);

    if (response.status === 401) {
      throw new FetchError(this.#statusTypes.unauthorized());
    }

    if (!response.ok) {
      const data = await response.json();
      throw new FetchError(this.#statusTypes.badRequest(), data);
    }

    return response;
  }

  #validate(method, path) {
    if (!method || !path) {
      throw new FetchError("Request must have method and path");
    }
  }

  #logoutIfUnauthorized() {
    setTimeout(() => {
      window.location.reload(false);
    }, 100);
  }
}

export default Request;
