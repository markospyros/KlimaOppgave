import axios from "axios";
import React, { useState } from "react";

const AnswerInput = (props) => {
  const [svarText, setSvarText] = useState("");
  const [buttonState, setButtonState] = useState("btn-secondary disabled");
  const [inputState, setInputState] = useState("border-secondary");
  const [errorMessageStatus, setErrorMessageStatus] = useState("none");

  const onFocus = () => {
    setInputState("border-primary");
    const svarTextWithoutSpace = svarText.trim();

    if (svarTextWithoutSpace.length !== 0) {
      setButtonState("btn-primary");
    }
  };

  const onBlur = () => {
    setInputState("border-secondary");
    setErrorMessageStatus("none");

    const svarTextWithoutSpace = svarText.trim();

    if (svarTextWithoutSpace.length === 0) {
      setButtonState("btn-secondary disabled");
    }
  };

  const onChangeText = (event) => {
    setInputState("border-primary");
    setErrorMessageStatus("none");

    const inputValue = event.target.value;
    setSvarText(inputValue);

    if (inputValue.trim().length === 0) {
      setButtonState("btn-secondary disabled");
    } else {
      setButtonState("btn-primary");
    }
  };

  const submitForm = (event) => {
    const svarTextWithoutSpace = svarText.trim();
    if (svarTextWithoutSpace.length !== 0) {
      const svar = {
        innhold: svarText,
        innleggId: props.innleggId,
      };

      axios.post("/leggsvar", svar).then((response) => {
        svar.svarId = response.data.svarId;
        svar.dato = response.data.dato;
        props.addAnswer(svar, svar.innleggId);
      });

      setSvarText("");
    } else {
      setInputState("border-danger");
      setErrorMessageStatus("block");
    }

    event.preventDefault();
  };

  return (
    <div className="card-footer">
      <form onSubmit={submitForm} className="input-group">
        <input
          value={svarText}
          onFocus={onFocus}
          onBlur={onBlur}
          onChange={onChangeText}
          type="text"
          className={`form-control form-control-outline ${inputState} border-3 shadow-none`}
          placeholder="Legg inn et svar"
        />

        <button
          className={`btn ${buttonState}`}
          type="button"
          onClick={submitForm}
        >
          Svar
        </button>
      </form>
      <div className={`invalid-feedback d-${errorMessageStatus}`}>
        Svar må fylles ut
      </div>
    </div>
  );
};

export default AnswerInput;
