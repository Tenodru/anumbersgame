using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// -----------------------------------------------------------------------------------------------------------
// 
// This is a collection of useful UI classes and other code for display purposes. Continually expanded.
// This version uses TextMeshPro for text purposes.
// This is part of the growing Ascent collection, a library of helpful code meant to make developing
// mechanics and implementing systems in Unity games easier.
//
// Created by: Alex Kong.
// Creation Date: 05/01/2020
// Last Updated: 05/09/2020
//
// -----------------------------------------------------------------------------------------------------------

namespace AscentUI
{
    namespace Elements
    {
        [System.Serializable]
        /// <summary>
        /// A Text-based stat display element.
        /// </summary>
        public class DisplayElementText
        {
            /// <summary>
            /// The label text component of this DisplayElement. This will not change on DisplayElement updates.
            /// </summary>
            [Tooltip("The label text of this DisplayElement. Will not change on stat updates.")]
            public string label = "";
            /// <summary>
            /// The text display component of this DisplayElement This is the entire text element.
            /// </summary>
            [Tooltip("The TextMeshPro object to be referenced.")]
            public TextMeshProUGUI displayText;
            /// <summary>
            /// The background component of this DisplayElement, if there is one.
            /// </summary>
            [Tooltip("The optional background component of this DisplayElement.")]
            public GameObject background;

            /// <summary>
            /// Creates a new Text DisplayElement with a background component.
            /// </summary>
            /// <param name="newLabel">The label of this element. Does not change on updates.</param>
            /// <param name="newText">The TextMeshPro object.</param>
            /// <param name="bg">The background object of this element.</param>
            public DisplayElementText(string newLabel, TextMeshProUGUI newText, GameObject bg)
            {
                label = newLabel;
                displayText = newText;
                background = bg;
            }

            /// <summary>
            /// Creates a new Text DisplayElement without a background component.
            /// </summary>
            /// <param name="newLabel">The label of this element. Does not change on updates.</param>
            /// <param name="newText">The TextMeshPro object.</param>
            public DisplayElementText(string newLabel, TextMeshProUGUI newText)
            {
                label = newLabel;
                displayText = newText;
            }

            /// <summary>
            /// Updates the textfield with the new specified amount.
            /// </summary>
            /// <param name="newAmount">The new amount to be displayed.</param>
            public void UpdateText(float newAmount)
            {
                displayText.text = (label + newAmount);
            }
        }

        [System.Serializable]
        /// <summary>
        /// A Bar-based stat display element.
        /// </summary>
        public class DisplayElementBar
        {
            /// <summary>
            /// The bar component of this DisplayElement.
            /// </summary>
            [Tooltip("The bar component of this DisplayElement.")]
            public RectTransform bar;
            /// <summary>
            /// The label text component of this DisplayElement. This will not change on DisplayElement updates.
            /// </summary>
            [Tooltip("The label text of this DisplayElement. Will not change on stat updates.")]
            public string label = "";
            /// <summary>
            /// The bar component of this DisplayElement.
            /// </summary>
            [Tooltip("The TextMeshPro object to be referenced.")]
            public TextMeshProUGUI displayText;
            /// <summary>
            /// The background component of this DisplayElement, if there is one.
            /// </summary>
            [Tooltip("The optional background component of this DisplayElement.")]
            public GameObject background;

            [HideInInspector] public float barDefaultHeightSize;
            [HideInInspector] public float barDefaultWidthSize;
            [HideInInspector] public float barHeightSize;
            [HideInInspector] public float barWidthSize;

            /// <summary>
            /// Creates a new Bar DisplayElement.
            /// </summary>
            /// <param name="newBar">The Bar RectTransform.</param>
            /// <param name="bg">The background component.</param>
            public DisplayElementBar(RectTransform newBar, GameObject bg)
            {
                bar = newBar;
                background = bg;

                barDefaultHeightSize = bar.sizeDelta.y;
                barDefaultWidthSize = bar.sizeDelta.x;
                barHeightSize = bar.sizeDelta.y;
                barWidthSize = bar.sizeDelta.x;
            }

            /// <summary>
            /// Creates a new Bar DisplayElement without a background component.
            /// </summary>
            /// <param name="newBar">The Bar RectTransform.</param>
            public DisplayElementBar(RectTransform newBar)
            {
                bar = newBar;

                barDefaultHeightSize = bar.sizeDelta.y;
                barDefaultWidthSize = bar.sizeDelta.x;
                barHeightSize = bar.sizeDelta.y;
                barWidthSize = bar.sizeDelta.x;
            }

            /// <summary>
            /// Creates a new Bar DisplayElement with text.
            /// </summary>
            /// <param name="newBar">The Bar RectTransform.</param>
            /// /// <param name="newLabel">The label of the text element. Does not change on update.</param>
            /// /// <param name="newDisplaytext">The TextMeshPro object.</param>
            /// <param name="bg">The background component.</param>
            public DisplayElementBar(RectTransform newBar, string newLabel, TextMeshProUGUI newDisplaytext, GameObject bg)
            {
                bar = newBar;
                label = newLabel;
                displayText = newDisplaytext;
                background = bg;

                barDefaultHeightSize = bar.sizeDelta.y;
                barDefaultWidthSize = bar.sizeDelta.x;
                barHeightSize = bar.sizeDelta.y;
                barWidthSize = bar.sizeDelta.x;
            }

            /// <summary>
            /// Creates a new Bar DisplayElement with text without a background component.
            /// </summary>
            /// <param name="newBar">The Bar RectTransform.</param>
            /// /// <param name="newLabel">The label of the text element. Does not change on update.</param>
            /// /// <param name="newDisplaytext">The TextMeshPro object.</param>
            public DisplayElementBar(RectTransform newBar, string newLabel, TextMeshProUGUI newDisplaytext)
            {
                bar = newBar;
                label = newLabel;
                displayText = newDisplaytext;

                barDefaultHeightSize = bar.sizeDelta.y;
                barDefaultWidthSize = bar.sizeDelta.x;
                barHeightSize = bar.sizeDelta.y;
                barWidthSize = bar.sizeDelta.x;
            }

            /// <summary>
            /// Changes the specified attribute of this Bar by the specified amount.
            /// </summary>
            /// <param name="attribute">The attribute of the Bar to change.</param>
            /// <param name="changeAmount">The amount to change by (additive).</param>
            public void UpdateBar(BarAttribute attribute, float changeAmount)
            {
                if (attribute == BarAttribute.Height)
                {
                    bar.sizeDelta = new Vector2(barWidthSize, barHeightSize + changeAmount);
                    barHeightSize = bar.sizeDelta.y;
                }
                if (attribute == BarAttribute.Width)
                {
                    bar.sizeDelta = new Vector2(barWidthSize + changeAmount, barHeightSize);
                    barWidthSize = bar.sizeDelta.x;
                }
            }

            /// <summary>
            /// Changes the specified attribute of this Bar by the specified amount.
            /// </summary>
            /// <param name="attribute">The attribute of the Bar to change.</param>
            /// <param name="changeAmount">The amount to change by (additive).</param>
            /// <param name="newAmount">The new amount to be displayed.</param>
            public void UpdateBar(BarAttribute attribute, float changeAmount, float newAmount)
            {
                if (attribute == BarAttribute.Height)
                {
                    //bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, barHeightSize + changeAmount);
                    bar.sizeDelta = new Vector2(barWidthSize, barHeightSize + changeAmount);
                    barHeightSize = bar.sizeDelta.y;

                    displayText.text = (label + newAmount);
                }
                if (attribute == BarAttribute.Width)
                {
                    bar.sizeDelta = new Vector2(barWidthSize + changeAmount, barHeightSize);
                    barWidthSize = bar.sizeDelta.x;

                    displayText.text = (label + newAmount);
                }
            }

            /// <summary>
            /// Enables and disables this Display Element.
            /// </summary>
            /// <param name="enabled">Whether this Element is enabled.</param>
            public void IsEnabled(bool enabled)
            {
                if (!enabled)
                {
                    bar.gameObject.SetActive(false);
                    if (displayText != null)
                        displayText.gameObject.SetActive(false);
                    if (background != null)
                        background.SetActive(false);
                }
                else
                {
                    bar.gameObject.SetActive(true);
                    if (displayText != null)
                        displayText.gameObject.SetActive(true);
                    if (background != null)
                        background.SetActive(true);
                }
            }
        }

        /// <summary>
        /// A changeable attribute of a Bar DisplayElement.
        /// </summary>
        [System.Serializable]
        public enum BarAttribute
        {
            Height, Width
        }

        /// <summary>
        /// The orientation of this bar (how changes will be shown).
        /// </summary>
        [System.Serializable]
        public enum BarOrientation
        {
            Horizontal, Vertical
        }
    }
}
