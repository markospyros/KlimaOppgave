import React from "react";
import { useNavigate } from "react-router-dom";

const AskQuestionButton = () => {
  const navigate = useNavigate();

  return (
    <div
      className="btn btn-primary mb-3"
      style={{ width: "100%" }}
      onClick={() => navigate("/askquestion")}
    >
      Still et spørsmål
    </div>
  );
};

export default AskQuestionButton;
