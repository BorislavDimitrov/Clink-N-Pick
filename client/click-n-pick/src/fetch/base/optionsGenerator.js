import Auth from "./auth";

class OptionsGenerator {
  #auth = new Auth();

  generate(method, body, isFormData = false, isSendDataForm = false) {
    let options = {
      headers: {},
    };

    options = this.#setMethod(options, method);
    options = this.#setTokenIfExists(options);
    options = this.#setJsonContentTypeIfBody(options, body, isFormData);
    options = this.#setFormDataIfBodyFromData(
      options,
      body,
      isFormData,
      isSendDataForm
    );

    return options;
  }

  #setMethod(options, method) {
    options.method = method;
    return options;
  }

  #setFormDataIfBodyFromData(options, body, isFormData, isSendDataForm) {
    if (body && isFormData && isSendDataForm) {
      options.body = body;
      return options;
    }

    if (body && isFormData) {
      const formDataBody = new FormData();

      formDataBody.append("file", body);
      options.body = formDataBody;
    }

    return options;
  }

  #setJsonContentTypeIfBody(options, body, isFormData) {
    if (body && !isFormData) {
      options.headers["Content-Type"] = "application/json";
      options.body = JSON.stringify(body);
    }
    return options;
  }

  #setTokenIfExists(options) {
    if (this.#auth.getToken()) {
      options.headers.Authorization = `Bearer ${this.#auth.getToken()}`;
    }
    return options;
  }
}

export default OptionsGenerator;
