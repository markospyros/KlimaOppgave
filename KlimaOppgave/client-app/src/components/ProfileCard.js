import React from "react";

const ProfileCard = (props) => {
  return (
    <div className={`d-flex ${props.marginBottom}`}>
      <img
        src="https://st4.depositphotos.com/1000507/24488/v/600/depositphotos_244889634-stock-illustration-user-profile-picture-isolate-background.jpg"
        alt="profile_pic"
        style={{ width: `${props.picWidth}`, height: `${props.picHeight}` }}
        className="border border-1"
      />
      <div className={`mx-3 mt-2 fw-semibold ${props.fontSize}`}>
        {props.brukernavn}
      </div>
    </div>
  );
};

export default ProfileCard;
