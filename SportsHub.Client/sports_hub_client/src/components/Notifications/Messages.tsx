import React from 'react';

export const incorrectEmail = 'Incorrect email format'; 

export const emptyInput = (name?:string)=>{
    return name ? `Field ${name} is required` : `Field is required`;
}; 

export const minLength = (len:number)=>{
    return `Minimal length - ${len} symbols`
}; 

export const maxLength = (len:number)=>{
    return `Maximal length - ${len} symbols`
}; 

export const successfulDeleteAction = (name:string, itemName?:string)=>{
    return itemName ? `${name} ${itemName} is deleted` : `${name} is deleted`;
}; 

export const shouldContain = (items:string)=>{
    return `Field must contain ${items}`;
};
